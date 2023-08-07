
const assetTranferChose = {
     namespaced: true,
     state: {
          listAssetTranferChose: [],
          isLoadingTableTranfer: false,
          listSelected: [],
          listAssetFirst: []
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

          setListAssetFirst(state, listAssetFirst) {
               state.listAssetFirst = listAssetFirst;
          },

          /**
           * Function set loading table
           * @param {*} state 
           * @param {*} isLoadingTableTranfer 
           * Author: HMDUC (28/07/2023)
           */
          setLoadingTableTranfer(state, isLoadingTableTranfer) {
               state.loadingTableTranfer = isLoadingTableTranfer;
          },

          /**
           * Mutations set listSelected
           * @param {*} state 
           * @param {*} listSelected 
           * Author: HMDUC (28/07/2023)
           */
          setListSelected(state, listSelected) {
               state.listSelected = listSelected;
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
          },

          /**
           * Function set listSelected
           * Author: HMDUC (28/07/2023)
           */
          setListSelected({ commit }, listSelected) {
               commit("setListSelected", listSelected)
          },

          setListAssetFirst({ commit }, listAssetFirst) {
               commit("setListAssetFirst", listAssetFirst)
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
          },

          /**
           * Get listSelected
           * Author: HMDUC (28/07/2023)  
           */
          listSelected(state) {
               return state.listSelected;
          },

          listAssetFirst(state) {
               return state.listAssetFirst;
          }
     }

}

export default assetTranferChose;