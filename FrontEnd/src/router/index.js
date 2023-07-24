import { createRouter, createWebHistory } from "vue-router";
import AssetList from '@/view/asset/AssetList'
import ReportPage from '@/view/report/ReportPage'

const routers = [
    {
        path: '/',
        redirect: '/asset'
    },
    {
        path: '/asset',
        component: AssetList,
    },
    {
        path: '/assetincrease',
        component: AssetList
    },
    {
        path: '/changeinfor',
        component: AssetList
    },
    {
        path: '/assetassessment',
        component: AssetList
    },
    {
        path: '/tranferasset',
        component: AssetList
    },
    {
        path: '/receiptreccommend',
        component: AssetList
    },
    {
        path: '/assetreducing',
        component: AssetList
    },
    {
        path: '/depreciationbusiness',
        component: AssetList
    },
    {
        path: '/depreciationasset',
        component: AssetList
    },
    {
        path: '/assetinventory',
        component: AssetList
    },
    {
        path: '/tai-san-ht',
        component: ReportPage,
    },
    {
        path: '/công-cụ',
    },
    {
        path: '/danh-mục',

    },

];

const vueRouter = createRouter({
    history: createWebHistory(),
    routes: routers
})

export default vueRouter;