import { createStore } from 'vuex'

import formDialog from './formDialog';
import contextMenu from './contextMenu';
import asset from './asset';
import yearSelected from './yearSelected';
import sideBar from './sideBar';
import inputError from './inputError';
import displayTable from './displayTable';


/**
 * Create store
 * Author: HMDUC (29/05/2023)
 */
const store = createStore({
    modules: {
        formDialog,
        contextMenu,
        asset,
        yearSelected,
        sideBar,
        inputError,
        displayTable
    }

})

export default store;