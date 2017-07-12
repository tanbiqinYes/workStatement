import axios from 'axios';
import { Message } from 'element-ui';
import store from '../store';
import utils from 'axios/lib/utils';
import normalizeHeaderName from 'axios/lib/helpers/normalizeHeaderName';
import qs from 'qs';
// import router from '../router';

// 创建axios实例
const service = axios.create({
  baseURL: process.env.BASE_API, // api的base_url
  // timeout: 5000                  // 请求超时时间 影响请求post 有几率报错Provisional headers are shown
});

// request拦截器
service.interceptors.request.use(config => {
  // Do something before request is sent
  if (store.getters.access_token) {
    config.headers['Authorization'] = "bearer "+store.getters.access_token; // 让每个请求携带token--['X-Token']为自定义key 请根据实际情况自行修改
  }
  // function setContentTypeIfUnset(headers, value) {
  //   if (!utils.isUndefined(headers) && utils.isUndefined(headers['Content-Type'])) {
  //     headers['Content-Type'] = value;
  //   }
  // }

  // config.transformRequest = [function (data, headers) {
  //   normalizeHeaderName(headers, 'Content-Type');
  //
  //   if (utils.isFormData(data) ||
  //     utils.isArrayBuffer(data) ||
  //     utils.isStream(data) ||
  //     utils.isFile(data) ||
  //     utils.isBlob(data)
  //   ) {
  //     return data;
  //   }
  //
  //   if (utils.isArrayBufferView(data)) {
  //     return data.buffer;
  //   }
  //   if (utils.isURLSearchParams(data)) {
  //     setContentTypeIfUnset(headers, 'application/x-www-form-urlencoded;charset=utf-8');
  //     return data.toString();
  //   }
  //   if (utils.isObject(data)) {
  //     setContentTypeIfUnset(headers, 'application/json;charset=utf-8');
  //     if (config.method != "get" && config.method != "put"){
  //       config.headers['Content-Type'] = "application/x-www-form-urlencoded;charset=utf-8";
  //       // let ret = '';
  //       // let arrayKey = Object.keys(data);
  //       //
  //       // arrayKey.forEach(function(it,index) {
  //       //     if (index < arrayKey.length - 1){
  //       //       ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&';
  //       //     }
  //       //     else{
  //       //       ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]);
  //       //     }
  //       // })
  //       return qs.stringify(data);
  //     }
  //
  //     return JSON.stringify(data);
  //   }
  //   return data;
  // }]


  // if (config.method != "get"){
  //   config.headers['Content-Type'] = "application/json";
  //   config.transformRequest = [function (data) {
  //   //       // Do whatever you want to transform the data
  //           let ret = ''
  //           let arrayKey = Object.keys(data);
  //
  //           arrayKey.forEach(function(it,index) {
  //               if (index < arrayKey.length - 1){
  //                 ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]) + '&';
  //               }
  //               else{
  //                 ret += encodeURIComponent(it) + '=' + encodeURIComponent(data[it]);
  //               }
  //           })
  //           return ret
  //           // var ss = JSON.stringify(data);
  //           // console.log(ss);
  //           // return ss;
  //         }];
  // }
// console.log(config)
  return config;
  }, error => {
  // Do something with request error
  console.log(error); // for debug
  Promise.reject(error);
})

// respone拦截器
service.interceptors.response.use(
  response => response
  /**
  * 下面的注释为通过response自定义code来标示请求状态，当code返回如下情况为权限有问题，登出并返回到登录页
  * 如通过xmlhttprequest 状态码标识 逻辑可写在下面error中
  */
  // const code = response.data.code;
  // // 50014:Token 过期了 50012:其他客户端登录了 50008:非法的token
  // if (code === 50008 || code === 50014 || code === 50012) {
  //   Message({
  //     message: res.message,
  //     type: 'error',
  //     duration: 5 * 1000
  //   });
  //   // 登出
  //   store.dispatch('FedLogOut').then(() => {
  //     router.push({ path: '/login' })
  //   });
  // } else {
  //   return response
  // }
  // ,
  // error => {
  //   // 请求已发出，但服务器响应的状态码不在 2xx 范围内
  //     // console.log(error.response.data);
  //     // console.log(error.response.status);
  //     // console.log(error.response.headers);
  //   console.log(error.response.data);// for debug
  //   if (error.response){
  //     Message({
  //       message: error.response.data,
  //       type: 'error',
  //       duration: 5 * 1000
  //     });
  //   }
  //   return Promise.reject(error);
  // }
)

export default service;
