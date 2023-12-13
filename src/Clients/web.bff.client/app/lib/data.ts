import { unstable_noStore as noStore } from 'next/cache';

export async function fetchChatMessages(page: number, take: number) {
  noStore();
  return fetch(`http://localhost:10001/api/chatmessages?page=${page}&take=${take}`)
  .then((response) => response.json())
  .catch(error => console.error(error));
}

export async function fetchStatistics(page: number, take: number, type: string) {
  noStore();
  return fetch(`http://localhost:10001/api/chatmessages/statistics?page=${page}&take=${take}&type=${type}`)
  .then((response) => response.json())
  .catch(error => console.error(error));
}
