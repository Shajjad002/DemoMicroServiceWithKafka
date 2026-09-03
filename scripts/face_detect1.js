import http from 'k6/http';
import { check } from 'k6';

const payload = open('/scripts/payload.json');

export const options = {
    stages: [
        { duration: '1m', target: 10 },
        { duration: '2m', target: 25 },
        { duration: '2m', target: 50 },
        { duration: '2m', target: 75 },
        { duration: '2m', target: 100 },
        { duration: '1m', target: 0 },
    ],

    thresholds: {
        http_req_failed: ['rate<0.05'],
        http_req_duration: [
            'p(90)<2000',
            'p(95)<3000',
            'p(99)<5000',
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
        }
    );

    check(response, {
        'HTTP 200': (r) => r.status === 200,
        'response received': (r) => r.body && r.body.length > 0,
    });
}