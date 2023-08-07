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
          assetEdit: [],
          receiptEdit: {},
          receiptError: {},
          listAssetDelete: [],
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

          /**
           * Mutations set loading TableDetail
           * @param {*} state 
           * @param {*} isLoadingDetail 
           */
          setLoadingDetail(state, isLoadingDetail) {
               state.isLoadingDetail = isLoadingDetail;
          },

          /**
           * Mutations list AssetEdit
           * @param {*} state 
           * @param {*} list 
           */
          setAssetEdit(state, list) {
               state.assetEdit = list;
          },

          /**
           *  Mutations set list Receipt Edit
           * @param {*} state 
           * @param {*} receiptEdit 
           */
          setReceiptEdit(state, receiptEdit) {
               state.receiptEdit = receiptEdit;
          },

          /**
           * Mutatiosn set state Receipt Error
           * @param {*} state 
           * @param {*} error 
           */
          setReceiptError(state, error) {
               state.receiptError = error;
          },

          /**
           * Mutatiosn set state List Asset Delete
           * @param {*} state 
           * @param {*} error 
           */
          setListAssetDelete(state, list) {
               state.listAssetDelete = list
          }
     },
     actions: {
          /**
           * Function call Api getReceiptList 
           * @param {*} param0 
           * @param {*} param1 
           * Author: HMDUC (31/07/2023) 
           */
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

          /**
         * Function call Api  getListAsset Edit
         * @param {*} param0 
         * @param {*} param1 
         * Author: HMDUC (31/07/2023) 
         */
          async getAssetListEdit({ commit }, id) {
               try {
                    const res = await request.getAssetEdit(id);
                    commit("setAssetEdit", res);
               } catch (err) {
                    console.log(err);
               }
          },

          /**
          * Function call Api  delete one Receipt
          * @param {*} param0 
          * @param {*} param1 
          * Author: HMDUC (31/07/2023) 
          */
          async deleteOnlyReceipt({ dispatch }, id) {
               try {
                    const res = await request.deleteReceipt(id);
                    //update receipt;
                    dispatch("getReceiptList");
                    return res.status;
               } catch (err) {
                    return err.response;
               }
          },

          /**
           * Function call Api add NewReceipt 
           * @param {*} param0 
           * @param {*} data 
           * Author: HMDUC (31/07/2023) 
           */
          async addNewReceipt({ dispatch }, data) {
               try {
                    const res = await request.postReceipt(data);
                    dispatch("getReceiptList");
                    return res;
               } catch (err) {
                    return err.response;
               }
          },

          /**
           * Function call Api update receipt
           * @param {*} param0 
           * @param {*} data 
           * Author: HMDUC (03/08/2023)
           */
          async updateOldReceipt({ dispatch }, data) {
               try {
                    const res = await request.updateNewReceipt(data);
                    dispatch("getReceiptList");
                    return res;
               } catch (err) {
                    return err.response;
               }
          },

          /**
           * Functions set ListSelectClick
           * @param {*} param0 
           * @param {*} listSelectClick 
           * Author: HMDUC (31/07/2023) 
           */
          setListSelectClick({ commit }, listSelectClick) {
               commit("setListSelectClick", listSelectClick)
          },

          /**
           * Function set ListReceiptSelected
           * @param {*} param0 
           * @param {*} listReceiptSelected 
           * Author: HMDUC (31/07/2023) 
           */
          setListReceiptSelected({ commit }, listReceiptSelected) {
               commit("setListReceiptSelected", listReceiptSelected)
          },

          /**
           * Function set AssetEdit
           * @param {*} param0 
           * @param {*} assetEdit 
           * Author: HMDUC (31/07/2023) 
           */
          setAssetEdit({ commit }, assetEdit) {
               commit("setAssetEdit", assetEdit)
          },

          /**
          * Function set ReceitpEdit
          * @param {*} param0 
          * @param {*} receiptEdit 
          * Author: HMDUC (31/07/2023) 
          */
          setReceiptEdit({ commit }, receiptEdit) {
               commit("setReceiptEdit", receiptEdit)
          },

          /**
           * Function set ReceiptError
           * @param {*} param0 
           * @param {*} error 
           * Author: HMDUC (31/07/2023) 
           */
          setReceiptError({ commit }, error) {
               commit("setReceiptError", error)
          },

          setListAssetDelete({ commit }, list) {
               commit("setListAssetDelete", list)
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
          },

          receiptEdit(state) {
               return state.receiptEdit;
          },

          assetEdit(state) {
               return state.assetEdit;
          },

          receiptError(state) {
               return state.receiptError;
          },

          listAssetDelete(state) {
               return state.listAssetDelete;
          }
     }
}

export default receipt;