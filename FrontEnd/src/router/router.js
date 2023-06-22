import { createRouter, createWebHistory } from "vue-router";
import PropertyList from "@/view/property/PropertyList.vue";

const routers = [
    {
        path: '/',
        redirect: '/tai-san'
    },
    {
        path: '/tai-san',
        component: PropertyList,
        meta: { title: 'Tài sản' }
    },
    {
        path: '/tai-san-ht',
        meta: { title: 'Tài sản HT-ĐB' }
    },
    {
        path: '/công-cụ',
        meta: { title: 'Công cụ dụng cụ' }
    },
    {
        path: '/danh mục',
        meta: { title: 'Danh mục' }
    },

];

const vueRouter = createRouter({
    history: createWebHistory(),
    routes: routers
})

export default vueRouter;