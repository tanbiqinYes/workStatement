import fetch from 'utils/fetch';

export function getResult() {
  return fetch({
    url: '/article/list',
    method: 'get'
  });
}


