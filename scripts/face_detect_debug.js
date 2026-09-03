import http from 'k6/http';
import { check } from 'k6';

const payload = open('/scripts/payload.json');

const url = 'https://ai.shakti.org.bd/face/detect_faces';

export const options = {
    vus: 1,
    duration: '10s',
};

export default function () {

    const response = http.post(url, payload, {
        headers: {
            'Content-Type': 'application/json',
        },
    });

    console.log(`Status: ${response.status}`);
    console.log(`Body: ${response.body}`);

    check(response, {
        'HTTP 200': (r) => r.status === 200,
    });
}