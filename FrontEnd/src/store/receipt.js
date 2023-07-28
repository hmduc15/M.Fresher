import { request } from "@/services/request";

const receipt = {
     namespaced: true,
     state: {
          loadingTime: null,
          isLoading: false,
          receiptList: [],
          listSelectClick: [],
          listReceiptSelected: [],
          reLoad: false,
          isLoadingDetail: false,
     },
     mutations: {
          /**
          * Mutations set state isLoading
          * Author: HMDUC(26/07/2023)
          * @param {*} state 
          * @param {*} isLoading 
          */
          setLoading(state, isLoading) {
               state.isLoading = isLoading
          },
          /**
          * Mutations set state assetList
          * Author: HMDUC(26/07/2023)
          * @param {*} state 
          * @param {*} assetList 
          */
          setReceiptList(state, receiptList) {
               state.receiptList = receiptList;
          },

          /**
           * Mutations set list Receipt Selected
           * Author: HMDUC(26/07/2023)
           * @param {*} state 
           * @param {*} listReceiptSelected 
           */
          setListReceiptSelected(state, listReceiptSelected) {
               state.listReceiptSelected = listReceiptSelected;
          },

          /**
           * Mutations set list List Selected Click
           * @param {*} state 
           * @param {*} listSelectClick 
           */
          setListSelectClick(state, listSelectClick) {
               state.listSelectClick = listSelectClick;
          },

          setLoadingDetail(state, isLoadingDetail) {
               state.isLoadingDetail = isLoadingDetail
          }
     },
     actions: {
          async getReceiptList({ commit, dispatch }, { pageNumber = 1, pageSize = 20, searchInput = null, departmentName = null, categoryName = null } = {}) {
               commit("setLoading", true);
               try {
                    const res = await request.paggingTableTranferReceipt(pageNumber, pageSize, searchInput, departmentName, categoryName);
                    commit("setListSelectClick", [res.data[0]]);
                    dispatch('assetTranfer/getAssetTranferList', { pageNumber: 1, pageSize: 20, id: res.data[0].ReceiptId }, { root: true });
                    commit("setReceiptList", res);
               } catch (err) {
                    return err.response;
               } finally {
                    setTimeout(() => {
                         commit("setLoading", false);
                    }, 800);
               }
          },

          setListSelectClick({ commit }, listSelectClick) {
               commit("setListSelectClick", listSelectClick)
          },

          setListReceiptSelected({ commit }, listReceiptSelected) {
               commit("setListReceiptSelected", listReceiptSelected)
          }
     },
     getters: {
          /**
          * Get state loading Time
          * Author: HMDUC(26/07/2023)
          */
          loadingTime(state) {
               return state.loadingTime;
          },
          /**
           * Get state isLoading
           * Author: HMDUC(26/07/2023) 
           */
          isLoading(state) {
               return state.isLoading;
          },
          /**
           * Get state receipt list
           * Author: HMDUC(26/07/2023) 
           */
          receiptList(state) {
               return state.receiptList;
          },
          /**
           * Get list Receipt Selected
           * Author: HMDUC(26/07/2023) 
           */
          listReceiptSelected(state) {
               return state.listReceiptSelected;
          },

          /**
           * Get Lis Select click 
           * Author: HMDUC(26/07/2023) 
           */
          listSelectClick(state) {
               return state.listSelectClick;
          }

     }
}

export default receipt;