import http from "k6/http";
import { sleep } from "k6";

export const options = {
  vus: 5,
  duration: "30s",
};

export default function () {
  http.get("https://sandboxerp.shakti.org.bd:8070/api/Home/internallogin?username=15241&password=123456");
  sleep(1);
}