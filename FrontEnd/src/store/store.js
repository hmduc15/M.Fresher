import { createStore } from 'vuex'

import formDialog from './formDialog';
import contextMenu from './contextMenu';
import property from './property';
import toastMessage from './toastMessage';
import yearSelected from './yearSelected';
import sideBar from './sideBar';

const store = createStore({
    modules: {
        formDialog,
        contextMenu,
        property,
        toastMessage,
        yearSelected,
        sideBar
    }

})

export default store;