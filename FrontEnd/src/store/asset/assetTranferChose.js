
const assetTranferChose = {
     namespaced: true,
     state: {
          listAssetTranferChose: [],
          isLoadingTableTranfer: false,
     },
     mutations: {
          /**
           * Function set list AssetTranferChose
           * @param {*} state 
           * @param {*} listAssetTranferChose 
           * Author: HMDUC (28/07/2023)
           */
          setListAssetTranferChose(state, listAssetTranferChose) {
               state.listAssetTranferChose = listAssetTranferChose;
          },

          /**
           * Function set loading table
           * @param {*} state 
           * @param {*} isLoadingTableTranfer 
           * Author: HMDUC (28/07/2023)
           */
          setLoadingTableTranfer(state, isLoadingTableTranfer) {
               state.loadingTableTranfer = isLoadingTableTranfer;
          }
     },
     actions: {
          /**
           * Function set list AssetTranferChose
             * Author: HMDUC (28/07/2023
           */
          setListAssetTranferChose({ commit }, list) {
               commit("setListAssetTranferChose", list)
          },

          /**
          * Function set loading table
          * Author: HMDUC (28/07/2023)
          */
          setLoadingTableTranfer({ commit }, isLoading) {
               commit("setLoadingTableTranfer", isLoading)
          }
     },
     getters: {
          /**
           * Get list asset Tranfer chose
           * Author: HMDUC (28/07/2023)
           */
          listAssetTranferChose(state) {
               return state.listAssetTranferChose;
          },

          /**
           * Get isLoadingTableTranfer
           * Author: HMDUC (28/07/2023) 
           */
          isLoadingTableTranfer(state) {
               return state.isLoadingTableTranfer;
          }
     }
}

export default assetTranferChose;