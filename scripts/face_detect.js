import http from 'k6/http';
import { check } from 'k6';

const payload = open('/scripts/payload.json');

const url = 'https://ai.shakti.org.bd/face/detect_faces';

export const options = {
    vus: 10,
    duration: '1m',

    thresholds: {
        http_req_failed: ['rate<0.05'],
        http_req_duration: ['p(95)<3000'],
    },
};

export default function () {
    const response = http.post(url, payload, {
        headers: {
            'Content-Type': 'application/json',
        },
    });

    check(response, {
        'HTTP 200': (r) => r.status === 200,
        'Response received': (r) => r.body && r.body.length > 0,
    });
}