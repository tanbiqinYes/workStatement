const getters = {
  sidebar: state => state.app.sidebar,
  access_token: state => state.user.access_token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  roleName: state => state.user.roleName,
  uid: state => state.user.uid,
  email: state => state.user.email,
  introduction: state => state.user.introduction,
  auth_type: state => state.user.auth_type,
  status: state => state.user.status,
  roles: state => state.user.roles,
  setting: state => state.user.setting,
  permission_routers: state => state.permission.routers,
  addRouters: state => state.permission.addRouters,
  departOptions: state => state.roleInfo.depart,
  roleOptions: state => state.roleInfo.role,
  levelOptions: state => state.roleInfo.userlevel
};
export default getters
