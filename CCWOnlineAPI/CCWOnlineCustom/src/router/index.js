import Vue from 'vue';
import Router from 'vue-router';

/* layout */
import Layout from '../views/layout/Layout';

/* login */
import Login from '../views/login/';
// const authRedirect = () => import('../views/login/authredirect');
// const sendPWD = () => import('../views/login/sendpwd');
// const reset = () => import('../views/login/reset');

/* dashboard */
// const ViewTodo = () => import('../views/dashboard/index');

/* work-report */
const WorkReport = () => import('../views/work-report/index');
const WorkReportList = () => import('../views/work-report/departmentList.vue');
const WorkReportDetails = () => import('../views/work-report/details.vue');
const WorkReportPerson = () => import('../views/work-report/personList.vue');


/* evaluation-result */
const Evaluation = () => import('../views/evaluation-result/index');
const EvaluationPersonal = () => import('../views/evaluation-result/personResult.vue');
const EvaluationDepartment = () => import('../views/evaluation-result/departmentResult.vue');

/*questions-management */
const Questions = () => import('../views/questions-management/index.vue');
const QuestionsEdite = () => import('../views/questions-management/questions-build.vue');
const QuestionsDetails = () => import('../views/questions-management/questions-details.vue');
const QuestionsAdd = () => import('../views/questions-management/add-paper.vue');


/* error page */
const Err404 = () => import('../views/error/404');
const Err401 = () => import('../views/error/401');

// 20170619 zhangzao
// todo list
import todoList from '../views/todoThing/todoThing'
import employeelist from '../views/employeeList/employeeList'
// departmentStaff
import departmentStaff from '../views/departmentStaff/departmentStaff'
// committeeStaff
import committeeCreative from '../views/committeeStaff/committeeCreative'
import committeeClient from '../views/committeeStaff/committeeClient'
import committeeStaffEnd from '../views/committeeStaff/committeeStaffEnd'
import committeeTest from '../views/committeeStaff/committeeTest'
import committeeShow from '../views/committeeStaff/committeeShow'
import committeeStaffFront from '../views/committeeStaff/committeeStaffFront'
import committeeEndOs from '../views/committeeStaff/committeeEndOs'
import committeeAllManage from '../views/committeeStaff/committeeAllManage'
import committeeCioso from '../views/committeeStaff/committeeCioso'
import committeeSony from '../views/committeeStaff/committeeSony'

// 考评操作
import selfsubject from '../views/evaluation-operate/subject'
import headSubject from '../views/evaluation-operate/headSubject'
import committeeSubject from '../views/evaluation-operate/committeeSubject'

// 考评关系
import relationship from '../views/relationship/relationship'

// 员工管理
import staffManagement from '../views/staff-management/index'
// 考核权重
import assessmentWeight from '../views/assessment-weight/index'
// 任务管理
import taskManagement from '../views/task-management/index'




Vue.use(Router);

/**
 * icon : the icon show in the sidebar
 * hidden : if hidden:true will not show in the sidebar
 * redirect : if redirect:noredirect will not redirct in the levelbar
 * noDropdown : if noDropdown:true will not has submenu
 * meta : { role: ['管理员'] }  will control the page role
 **/

export const constantRouterMap = [
	{ path: '/login', component: Login, hidden: true },
	// { path: '/authredirect', component: authRedirect, hidden: true },
	// { path: '/sendpwd', component: sendPWD, hidden: true },
	// { path: '/reset', component: reset, hidden: true },
	{ path: '/404', component: Err404, hidden: true },
	{ path: '/401', component: Err401, hidden: true },
	// {
	//   path: '/',
	//   component: Layout,
	//   redirect: '/todo',
	//   name: '首页',
	//   hidden: true
	// },
	{
		path: '/',
		component: Layout,
		redirect: '/todo/index',
		name: '首页',
		icon: 'chanpin',
		noDropdown: true,
		hidden: true,
		children: [
			{
				path: 'todo',
				name: '待办事项',
				component: todoList,
				meta: {
					role: ['普通员工', '部门主管','组长', '考评委员会', '管理员', 'editor', 'committeeManager'],
					sidebar: true
				}
			}
		]
	},
	// {
	// path: '/home',
	// component: Layout,
	// redirect: '/home/index',
	// icon: 'xinrenzhinan',
	// noDropdown: true,
	// children: [{ path: 'index', component: ViewTodo, name: '员工绩效考核系统' }]
	// }
]

export default new Router({
	// mode: 'history', //后端支持可开
	scrollBehavior: () => ({ y: 0 }),
	routes: constantRouterMap
});

export const asyncRouterMap = [
	{
		path: '/todo',
		component: Layout,
		redirect: 'noredirect',
		icon: 'chanpinpeizhi',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '待办事项',
				component: todoList,
				meta: {
					role: ['普通员工', '部门主管','组长', '管理员','考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'staffList',
				name: '待办事项员工列表页',
				component: employeelist,
				meta: {
					role: ['部门主管','组长', '考评委员会', 'committeeManager'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/department',
		component: Layout,
		redirect: '/department/creative',
		icon: 'fenleishezhi',
		name: '部门',
		meta: {
			role: ['考评委员会', 'committeeManager'],
			sidebar: true
		},
		children: [
			{
				path: 'creative/',
				name: '创意部',
				component: committeeCreative,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'client/',
				name: '客户部',
				component: committeeClient,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'back-end/',
				name: '工程部',
				component: committeeStaffEnd,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'test/',
				name: '测试部',
				component: committeeTest,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'show/',
				name: '数字互动与展示部',
				component: committeeShow,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'front-end/',
				name: '前端研发部',
				component: committeeStaffFront,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'endos/',
				name: '系统工程部',
				component: committeeEndOs,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'allmanage/',
				name: '综合管理部',
				component: committeeAllManage,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'cisco/',
				name: '思科现场',
				component: committeeCioso,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'sony/',
				name: '索尼现场',
				component: committeeSony,
				meta: {
					role: ['考评委员会', 'committeeManager'],
					sidebar: true
				}
			},

		]
	},
	{
		path: '/department-staff',
		component: Layout,
		redirect: 'noredirect',
		icon: 'kehuguanli',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '部门员工',
				component: departmentStaff,
				meta: {
					role: ['部门主管'],
					sidebar: true
				}
			}
		]
	},

	// 组长
	{
		path: '/group-staff',
		component: Layout,
		redirect: 'noredirect',
		icon: 'kehuguanli',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '小组员工',
				component: departmentStaff,
				meta: {
					role: ['组长'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/work-Report',
		component: Layout,
		redirect: 'noredirect',
		icon: 'dingdan',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '工作报告',
				component: WorkReport,
				meta: {
					role: '普通员工',
					sidebar: true
				}
			},
			{
				path: 'index',
				name: '工作报告',
				component: WorkReportList,
				meta: {
					role: ['部门主管','组长', '考评委员会'],
					sidebar: true
				}
			},
			{
				path: 'details/:id',
				name: '工作报告详情',
				component: WorkReportDetails,
				meta: {
					role: ['普通员工', '部门主管','组长', '考评委员会', 'committeeManager'],
					sidebar: true
				}
			},
			{
				path: 'person-list/:name/:uid',
				name: '工作报告列表',
				component: WorkReportPerson,
				meta: {
					role: ['部门主管','组长', '考评委员会', 'committeeManager'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/evaluation-result',
		component: Layout,
		redirect: 'noredirect',
		icon: 'shangyeyunying',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '考核结果',
				component: Evaluation,
				meta: {
					// role: ['普通员工','部门主管','组长','考评委员会','committeeManager'],
					role: '考评委员会',
					sidebar: true
				}
			},
			{
				path: 'all-check/:year/:qutor/:from',
				name: '所有部门考核结果',
				component: EvaluationDepartment,
				meta: {
					// role: ['普通员工','部门主管','组长','考评委员会','committeeManager'],
					role: '考评委员会',
					sidebar: true
				}
			},
			{
				path: 'detail',
				name: '考核结果',
				component: EvaluationPersonal,
				meta: {
					role: ['普通员工', '部门主管','组长'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/department-evaluation-result',
		component: Layout,
		redirect: 'noredirect',
		icon: 'xiaoximoban',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '部门考核结果',
				component: EvaluationDepartment,
				meta: {
					role: ['部门主管'],
					sidebar: true
				}
			}
		]
	},
	// 小组考核结果

	{
		path: '/group-evaluation-result',
		component: Layout,
		redirect: 'noredirect',
		icon: 'xiaoximoban',
		noDropdown: true,
		children: [
			{
				path: 'index',
				name: '小组考核结果',
				component: EvaluationDepartment,
				meta: {
					role: ['组长'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/task-management',
		component: Layout,
		redirect: 'noredirect',
		icon: 'xiaoximoban',
		noDropdown: true,
		meta: {
			role: ['管理员'],
			sidebar: true
		},
		children: [
			{
				path: 'index',
				name: '任务管理',
				component: taskManagement,
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/appraisal-management',
		component: Layout,
		redirect: 'noredirect',
		icon: 'shangyeyunying',
		// noDropdown: true,
		name: '考核管理',
		meta: {
			role: ['管理员'],
			sidebar: true
		},
		children: [
			{
				path: 'test-library',
				name: '题库管理',
				icon: 'dingdanguanli',
				component: Questions,
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			},
			{
				path: 'details',
				name: '题库详情',
				component: QuestionsDetails,
				hidden: true,
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			}
			,
			{
				path: 'edite',
				name: '编辑题目',
				component: QuestionsEdite,
				hidden: true,
				meta: {
					role: ['管理员'],
					sidebar: false
				}
			},
			{
				path: 'addPaper',
				name: '添加试卷',
				component: QuestionsAdd,
				hidden: true,
				meta: {
					role: ['管理员'],
					sidebar: false
				}
			},
			{
				path: 'relationship',
				name: '考评关系',
				component: relationship,
				icon:"suoyinguanli",
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			},
			{
				path: 'assessment-weight',
				name: '考核权重',
				component: assessmentWeight,
				icon:"pingjiaguanli",
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/staff-management',
		component: Layout,
		redirect: '/staff-management/index',
		icon: 'kehuguanli',
		noDropdown: true,
		meta: {
			role: ['管理员'],
			sidebar: true
		},
		children: [
			{
				path: 'index',
				name: '员工管理',
				component: staffManagement,
				meta: {
					role: ['管理员'],
					sidebar: true
				}
			}
		]
	},
	{
		path: '/evaluation',
		component: Layout,
		redirect: 'noredirect',
		icon: 'theme',
		noDropdown: true,
		meta: {
			role: ['普通员工', '部门主管','组长', '考评委员会'],
			sidebar: false
		},
		children: [
			{
				path: 'index',
				name: '考评',
				meta: {
					role: ['普通员工', '部门主管','组长', '考评委员会'],
					sidebar: false
				}
			},
			{
				path: '/staff-evaluation/:uid/:type',
				name: '员工能力考评',
				component: selfsubject,
				meta: {
					role: ['普通员工', '部门主管','组长'],
					sidebar: true
				}
			},
			{
				path: '/staff-trainEvaluation/:uid/:type',
				name: '员工修养考评',
				component: selfsubject,
				meta: {
					role: ['普通员工', '部门主管','组长' ],
					sidebar: true
				}
			},
			{
				path: '/manager-evaluation/:uid/:relation',
				name: '主管考评',
				component: headSubject,
				meta: {
					role: ['部门主管','组长'],
					sidebar: false
				}
			},
			{
				path: '/committee-evaluation/:uid',
				name: '能力修养考评',
				component: committeeSubject,
				meta: {
					role: ['考评委员会','部门主管','组长','普通员工'],
					sidebar: false
				}
			}
		]
	},

	{ path: '*', redirect: '/404', hidden: true }
];
