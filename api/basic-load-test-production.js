import http from 'k6/http';
import { check } from 'k6';
const BASE_API_URL = 'https://routefinder-service-1.n1ujnf2e69gi8.eu-west-1.cs.amazonlightsail.com';
const BASE_UI_URL = 'https://goroutefinderonline.co.uk'
export const options = {
    thresholds: { 
      http_req_failed: ['rate<0.01'], // http errors should be less than 1%
      http_req_duration: ['p(95)<1000'], // 95% of requests should be below 200ms
    },
    scenarios: {
        stress: {
          executor: "ramping-arrival-rate",
          preAllocatedVUs: 30,
          timeUnit: "1s",
          stages: [
            { duration: "2m", target: 10 }, // below normal load
            { duration: "5m", target: 10 },
            { duration: "2m", target: 10 }, // wind down
          ],
        },
      }, 
};

function TestLogin() {
    //test auth
    let url = `${BASE_API_URL}/api/auth`;

    let payload = JSON.stringify({
        email: 'testuser6@test.com',
        password: 'Password01',
      });   
    
    let params = {
    headers: {
        'Content-Type': 'application/json',
    },
    };

    let response = http.post(url, payload, params);
    let authToken = "";

    if (response.status == 200) {
        authToken = response.json().token;
    }

    check(response, {
        'is status 200': (r) => r.status === 200,
    });

    return authToken;
}

function testGetRoutes(authToken) {

    //test auth
    let url = `${BASE_UI_URL}/api/routes`;
    
    let params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authToken}`
        },
    };

    let response = http.get(url, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}

function testDashboard(authToken) {

    //test auth
    let url = `${BASE_UI_URL}`;
    
    let params = {
        headers: {
            'Authorization': `Bearer ${authToken}`
        },
    };

    let response = http.get(url, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}

function testRoutes(authToken) {

    //test auth
    let url = `${BASE_UI_URL}/routes`;
    
    let params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authToken}`
        },
    };

    let response = http.get(url, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}

function testGetUsers(authToken) {
    //test auth
    let url = `${BASE_API_URL}/api/users`;
    
    let params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${authToken}`
        },
    };

    let response = http.get(url, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}

export default function () {
    let authToken = TestLogin();
    testGetRoutes(authToken);
    testGetUsers(authToken);
    testDashboard(authToken);
}