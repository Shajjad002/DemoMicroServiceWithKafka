import http from 'k6/http';
import { check } from 'k6';

const payload = open('/scripts/payload.json');

export const options = {
    // Step-load: hold each VU level steady for a few minutes so you can see
    // latency at each concurrency level, instead of one continuous ramp.
    stages: [
        { duration: '30s', target: 5 },   // warm up
        { duration: '3m',  target: 5 },   // hold at 5 VUs
        { duration: '30s', target: 10 },
        { duration: '3m',  target: 10 },  // hold at 10 VUs
        { duration: '30s', target: 20 },
        { duration: '3m',  target: 20 },  // hold at 20 VUs
        { duration: '30s', target: 40 },
        { duration: '3m',  target: 40 },  // hold at 40 VUs
        { duration: '30s', target: 0 },   // ramp down
    ],

    thresholds: {
        // Keep error-rate as a real pass/fail gate.
        http_req_failed: ['rate<0.05'],

        // Report latency without aborting the whole run on failure.
        // abortOnFail:false means k6 still prints pass/fail per threshold,
        // but the run continues and you get full stage-by-stage data.
        http_req_duration: [
            { threshold: 'p(90)<2000', abortOnFail: false },
            { threshold: 'p(95)<3000', abortOnFail: false },
            { threshold: 'p(99)<5000', abortOnFail: false },
        ],
    },
};

const url = 'https://ai.shakti.org.bd/face/detect_faces';

export default function () {
    const response = http.post(
        url,
        payload,
        {
            headers: {
                'Content-Type': 'application/json',
            },
            // Tag requests with the current VU count so you can break down
            // latency by concurrency level afterward (e.g. in Grafana/InfluxDB).
            tags: { stage_vus: `${__VU}` },
        }
    );

    check(response, {
        'HTTP 200': (r) => r.status === 200,
        'response received': (r) => r.body && r.body.length > 0,
    });
}