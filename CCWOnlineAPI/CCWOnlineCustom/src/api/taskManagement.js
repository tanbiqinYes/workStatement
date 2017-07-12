import fetch from 'utils/fetch';

//权重列表--查询
export function GetAllTask(params) {
  return fetch({
    url: '/api/TaskManagement/GetAllTask',
    method: 'get',
    params
  });
}

//任务管理页--提交
export function PutTask(params) {
  return fetch.put('/api/TaskManagement/PutTask',params);
}
