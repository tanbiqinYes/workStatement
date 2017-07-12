import fetch from 'utils/fetch';

//普通员工工作报告页--根据时间获取数据、首次加载: StartDate EndDate
export function getPersonList(params) {
  return fetch({
    url: '/api/workreport/GetReportDataByTime',
    method: 'get',
    params
  });
}

//工作报告页---获取某个报告数据 Id
export function GetReportData(params) {
	return fetch({
		url: '/api/workreport/GetReportData',
		method: 'get',
		params
	});
}

//工作报告页--提交 AccountId Q1 Q2 Q3 Q4 Q5 Q6
export function PutReportData(params) {
	return fetch.put('/api/workReport/PutReportData',params);
}

//部门主管-工作报告--获取列表数据 name
export function GetDepartmentData(params) {
	return fetch({
		url: '/api/workreport/GetDepartmentData',
		method: 'get',
		params
	});
}
//部门主管-个人工作报告列表页--获取列表数据 name
export function GetSelfData(params) {
	return fetch({
		url: '/api/workreport/GetSelfData',
		method: 'get',
		params
	});
}
