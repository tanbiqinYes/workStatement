import fetch from 'utils/fetch';

//权重列表--查询
export function GetAllData(params) {
  return fetch({
    url: '/api/PercentConfig/GetAllData',
    method: 'get',
    params
  });
}

//权重页---编辑:Id,PercentName,PercentValue
export function PutConfigData(params) {
  return fetch.put('/api/PercentConfig/PutConfigData',params);
}
