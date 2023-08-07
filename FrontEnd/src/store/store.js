import { createStore } from 'vuex'

import formDialog from './formDialog';
import contextMenu from './contextMenu';
import asset from './asset/asset';
import yearSelected from './yearSelected';
import sideBar from './sideBar';
import inputError from './inputError';
import displayTable from './displayTable';
import receipt from './receipt';
import assetTranfer from './asset/assetTranfer';
import assetTranferChose from './asset/assetTranferChose';
import assetChose from './asset/assetChose';
import modalDialog from './modalDialog';
import loading from './loading';


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
        displayTable,
        receipt,
        assetTranfer,
        assetTranferChose,
        assetChose,
        modalDialog,
        loading
    }

})

export default store;