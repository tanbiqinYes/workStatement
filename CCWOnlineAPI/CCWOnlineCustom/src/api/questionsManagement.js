import fetch from 'utils/fetch';

export function getPaperList(params) {
  return fetch({
    url: '/api/examlist',
    method: 'get',
    params
  });
}
export function getPaperDetails(params) {
  return fetch('/api/examdetail',{params});
}
export function postPaperMessage(params) {
  return fetch.put('/api/examlist',params);
}

export function createdPaper(params) {
  return fetch({
    url: '/api/examlist',
    method: 'get',
    params
  });
}

export function deletePaperQuestion(params) {
  return fetch({
    url: '/api/examlist',
    method: 'delete',
    params
  });
}