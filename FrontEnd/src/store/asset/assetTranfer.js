import { request } from "@/services/request";

const assetTranfer = {
     namespaced: true,
     state: {
          listAssetTRanfer: [],
          isLoadingDetail: false,
     },
     mutations: {
          /**
           * Mutations set list Asset Tranfer
           * Author: HMDUC(26/07/2023)
           */
          seListAssetTRanfer(state, listAssetTRanfer) {
               state.listAssetTRanfer = listAssetTRanfer
          },

          /**
           * Mutations set list loading detail
           * Author: HMDUC(26/07/2023)
           */
          setLoadingDetail(state, isLoadingDetail) {
               state.isLoadingDetail = isLoadingDetail
          }
     },
     actions: {
          async getAssetTranferList({ commit }, { pageNumber = 1, pageSize = 20, id } = {}) {
               commit("setLoadingDetail", true);
               try {
                    const res = await request.paggingTableTranferAsset(id, pageNumber, pageSize);
                    commit("seListAssetTRanfer", res);
               } catch (err) {
                    return err.response;
               } finally {
                    setTimeout(() => {
                         commit("setLoadingDetail", false);
                    }, 800);
               }
          }
     },
     getters: {
          /**
           * Get state isLoadingDetail
           * @param {*} state 
           * Author: HMDUC(26/07/2023) 
           */
          isLoadingDetail(state) {
               return state.isLoadingDetail;
          },

          /**
           * Get state listAssetTranfer
           * @param {*} state 
           * Author: HMDUC(26/07/2023) 
           */
          listAssetTRanfer(state) {
               return state.listAssetTRanfer;
          }
     }
}

export default assetTranfer;