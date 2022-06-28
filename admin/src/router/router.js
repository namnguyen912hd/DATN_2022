import Vue from 'vue'
import VueRouter from 'vue-router'

import Login from '../views/user/login/Login.vue'

import ProductList from '../views/product/ProductList.vue'
import ProductDetail from '../views/product/ProductDetail.vue'

import CalculationUnitList from '../views/calculationunit/CalculationUnitList.vue'
import CalculationUnitDetail from '../views/calculationunit/CalculationUnitDetail.vue'

import UserList from '../views/user/UserList.vue'
import UserDetail from '../views/user/UserDetail.vue'

import ProductCategoryList from '../views/productcategory/ProductCategoryList.vue'
import ProductCategoryDetail from '../views/productcategory/ProductCategoryDetail.vue'

import Setting from '../views/setting/Setting.vue'
import Report from '../views/report/Report.vue'
import UnAuthen from '../views/user/UnAuthen.vue'
import RoleList from '../views/role/RoleList.vue'
import RoleDetail from '../views/role/RoleDetail.vue'

import OrderList from '../views/order/OrderList.vue'
import OrderDetail from '../views/order/OrderDetail.vue'
import Shipper from '../views/shipper/Shipper.vue'


Vue.use(VueRouter)

export const router = new VueRouter({
    mode: 'history',
    routes: [
        {path: '', redirect: '/login'},
        {path: '/Login', component: Login},

        {path: '/shipper', component: Shipper},
        // product
        {path: '/products', component: ProductList},
        {path: '/productdetail', component: ProductDetail},

        {path: '/calculationUnits', component: CalculationUnitList},
        {path: '/calculationUnitDetail', component: CalculationUnitDetail},

        {path: '/productCategories', component: ProductCategoryList},
        {path: '/productCategoryDetail', component: ProductCategoryDetail},

        {path: '/users', component: UserList},
        {path: '/userDetail', component: UserDetail},
        {path: '/unauthen', component: UnAuthen},
        {path: '/roles', component: RoleList},
        {path: '/roleDetail', component: RoleDetail},

        {path: '/orders', component: OrderList},
        {path: '/orderDetail', component: OrderDetail},

        {path: '/report', component: Report},
        {path: '/setting', component: Setting},
        
    ]
})