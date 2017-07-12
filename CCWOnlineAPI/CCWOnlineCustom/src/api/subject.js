import fetch from 'utils/fetch';

export function getUserSubject(params) {
  return fetch({
    url: '/api/answer',
    method: 'get',
    params
  });
}


export function Submit(params) {
  return fetch.post('/api/answer',params);
}