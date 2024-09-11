import http from 'k6/http';
import { sleep } from 'k6';

export default function () {
  http.get('https://localhost:7074/api/Loans?take=5&skip=0');
  sleep(1);
}