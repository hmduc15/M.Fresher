import { createApp } from 'vue';
import store from './store/store';
import escEvent from './directive/esc';
import moveRow from './directive/moveRow';
import clickOutside from './directive/clickOutisde';
import ctrlSave from './directive/ctrlSave';
import ctrlCancel from './directive/clickCancel';
import { MISAResources } from './utils/resource';
import mitt from 'mitt';
import vueRouter from './router';
import App from './App.vue'
import ElementPlus from 'element-plus';

// add css element ui
import 'element-plus/dist/index.css';
//css custom combobox to overide css elementui
import "../src/css/base/combobox.css";

const app = createApp(App, { mode: 'production' });
const emmiter = mitt();

// declare directive
app.directive('move-row', moveRow)
app.directive('esc', escEvent);
app.directive('clickOutside', clickOutside);
app.directive('ctrl-s', ctrlSave);
app.directive('ctrl-w', ctrlCancel)


//config devtool
app.config.devtools = true;

// declare component globaly
app.config.globalProperties.$_MISAResources = MISAResources;
app.config.globalProperties.$emmiter = emmiter;
app.config.performance = true;

app.use(ElementPlus);
app.use(store);
app.use(vueRouter);
app.mount('#app');
