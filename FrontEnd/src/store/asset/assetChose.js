import { request } from "@/services/request";

const assetChose = {
     namespaced: true,
     state: {
          listAssetChose: [],
          isLoadingTableChose: false,
          listAssetChoseSelected: [],
          listSelectecAssetClick: []
     },
     mutations: {
          /**
           * Mutations set list AssetChose
           * @param {*} state 
           * @param {*} listAssetChose 
           * Author: HMDUC (28/07/2023)
           */
          setListAssetChose(state, listAssetChose) {
               state.listAssetChose = listAssetChose
          },

          /**
           * Mutations set Loading Table AssetChose
           * @param {*} state 
           * @param {*} loadingTableChose 
           * Author: HMDUC (28/07/2023)
           */
          setLoadingTableChose(state, isLoadingTableChose) {
               state.isLoadingTableChose = isLoadingTableChose
          },

          /**
           * Mutations set list Selected
           * @param {*} state 
           * @param {*} listAssetChoseSelected 
           * Author: HMDUC (28/07/2023)
           */
          setListChoseSelected(state, listAssetChoseSelected) {
               state.listAssetChoseSelected = listAssetChoseSelected
          },

          /**
           * Mutations set list clickSelected
           * @param {*} state 
           * @param {*} listSelectecAssetClick 
           */
          setListSelectecAssetClick(state, listSelectecAssetClick) {
               state.listSelectecAssetClick = listSelectecAssetClick;
          }
     },
     actions: {
          async getListAssetChose({ commit }, { ids, pageNumber = 1, pageSize = 20 } = {}) {
               commit("setLoadingTableChose", true)
               try {
                    const res = await request.paggingAssetChose(ids, pageSize, pageNumber);
                    commit("setListAssetChose", res);
               } catch (err) {
                    return err.response;
               } finally {
                    commit("setLoadingTableChose", false);
               }
          },
          /**
          * Function set list Selected
          * @param {*} state 
          * @param {*} listAssetChoseSelected 
          * Author: HMDUC (28/07/2023)
          */
          setListChoseSelected({ commit }, list) {
               commit("setListChoseSelected", list)
          },

          /**
           * Function set List Selected Asset Click
           * @param {*} param0 
           * @param {*} list 
           */
          setListSelectecAssetClick({ commit }, list) {
               commit("setListSelectecAssetClick", list)
          }
     },
     getters: {
          /**
           * Get state listAssetChose
           * @param {*} state 
           * @returns 
           */
          listAssetChose(state) {
               return state.listAssetChose
          },

          /**
         * Get state listChoseSelected
         * @param {*} state 
         * @returns 
         */
          listChoseSelected(state) {
               return state.listChoseSelected
          },

          /**
          * Get state isLoadingTableChose
          * @param {*} state 
          * @returns 
          */
          isLoadingTableChose(state) {
               return state.isLoadingTableChose
          }

     }


}

export default assetChose;