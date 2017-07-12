import fetch from 'utils/fetch';

export function getTodoList(params) {
  return fetch({
    url: '/api/home/ReadyTask',
    method: 'get',
    params
  });
}