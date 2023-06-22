import { createApp } from 'vue';
import store from './store/store';
import clickOutside from './directive/clickOutside';
import escEvent from './directive/esc';
import { MISAResources } from './utils/resource';
import vueRouter from './router/router';
import App from './App.vue'
import ElementPlus from 'element-plus';


import 'element-plus/dist/index.css';
//css custom combobox to overide css elementui
import "../src/css/base/combobox.css";

const app = createApp(App);



app.directive('outside', clickOutside);
app.directive('esc', escEvent);

// declare component globaly
app.config.globalProperties.$_MISAResources = MISAResources;

app.use(ElementPlus);
app.use(store);
app.use(vueRouter);
app.mount('#app');
