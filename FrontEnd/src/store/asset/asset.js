import { request } from "@/services/request";

const asset = {
    namespaced: true,
    state: {
        loadingTime: null,
        isLoading: false,
        assetList: [],
        listSelected: [],
        reLoad: false,
    },
    mutations: {
        /**
         * Mutation set state ReLoad
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} reLoad 
         */
        setReload(state, reLoad) {
            state.reLoad = reLoad;
        },

        /**
         * Mutations set state isLoading
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} isLoading 
         */
        setLoading(state, isLoading) {
            state.isLoading = isLoading
        },

        /**
         * Mutations set state loadingTime
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} loadingTime 
         */
        setLoadingTime(state, loadingTime) {
            state.loadingTime = loadingTime;
        },

        /**
         * Mutations set state assetList
         * @param {*} state 
         * @param {*} assetList 
         */
        setAssetList(state, assetList) {
            state.assetList = assetList;
        },

        /**
         * Mutations set State listSelected
         * @param {*} state 
         * @param {*} listSelected 
         */
        setListSelected(state, listSelected) {
            state.listSelected = listSelected;
        }

    },
    actions: {
        /**
         * Function setloading, call api
         * Author: HMDUC(29/05/2023)
         * @param {Object} param0 
         * @param {*} param0.commit 
         * @param {Object} [param1={}] 
         * @param {number} [param1.pageNumber=1] 
         * @param {number} [param1.pageSize=20] 
         * @param {null} [param1.searchInput=null] 
         * @param {null} [param1.departmentName=null] 
         * @param {null} [param1.categoryName=null] 
         */
        async getAssetList({ commit }, { pageNumber = 1, pageSize = 20, searchInput = null, departmentName = null, categoryName = null } = {}) {
            commit("setLoading", true);
            try {
                const res = await request.paggingTable(pageNumber, pageSize, searchInput, departmentName, categoryName);
                commit("setAssetList", res);
            } catch (err) {
                return err.response;
            } finally {
                setTimeout(() => {
                    commit("setLoading", false);
                }, 1000);
            }
        },
        /**
        * Function delete asset by id
        * Author: HMDUC(29/05/2023)
        * @param {Object} param0 
        * @param {*} param0.dispatch 
        * @param {*} param0.commit  
        * @param {*} id 
        */
        async delete({ dispatch }, id) {
            try {
                const res = await request.deleteProperty(id);
                //update data after delete
                dispatch("getAssetList");
                return res.status;
            } catch (err) {
                return err.response;
            }
        },

        /**
         * Funtion delete multi 
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} listId 
         * @returns  statusCode
         */
        async deleteMulti({ dispatch, commit }, listId) {
            try {
                const res = await request.deleteMultiple(listId);
                commit("setReload", true)
                dispatch("getAssetList");
                return res.status;
            } catch (err) {
                return err.response;
            }
        },

        /**
         * Funtion add asset
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} data 
         * @returns sponse
         */
        async postAsset({ dispatch }, data) {
            try {
                const res = await request.postProperty(data);
                //update data after add asset
                dispatch("getAssetList");
                return res;
            } catch (err) {
                return err.response;
            }
        },

        /**
         * Funtion update asset
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} data 
         * @returns response
         */
        async updateAsset({ dispatch }, data) {
            try {
                const res = await request.updateProperty(data);
                //reload data after update asset
                dispatch("getAssetList");
                return res;
            } catch (err) {
                return err.response;
            }
        },

        /**
         * Funtion set list selected
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} listSelected 
         */
        setListSelected({ commit }, listSelected) {
            commit("setListSelected", listSelected);
        }

    },
    getters: {
        /**
        * Get state loading Time
        * Author: HMDUC(29/05/2023)
        */
        loadingTime(state) {
            return state.loadingTime;
        },

        /**
        * Get state assetList
        * Author: HMDUC(29/05/2023)
        */
        assetList(state) {
            return state.assetList;
        },

        /**
        * Get state list Selected
        * Author: HMDUC(29/05/2023)
        */
        listSelected(state) {
            return state.listSelected;
        },

        /**
        * Get state is Loading
        * Author: HMDUC(29/05/2023)
        */
        isLoading(state) {
            return state.isLoading;
        },

        /**
        * Get state reload
        * Author: HMDUC(29/05/2023)
        */
        reLoad(state) {
            return state.reLoad;
        }
    }
}

export default asset;