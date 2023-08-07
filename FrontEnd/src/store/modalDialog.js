const modalDialog = {
     namespaced: true,
     state: {
          isShow: false,
          modalMode: null,
     },
     mutations: {
          /**
           * Mutation set state isShow
           * Author: HMDUC(29/05/2023)
           * @param {*} state 
           * @param {*} isShow 
           */
          setIsShow(state, isShow) {
               state.isShow = isShow;
          },

          /**
           * Mutation set Modal Mode
           * @param {*} state 
           * @param {*} modalMode 
           */
          setModalMode(state, modalMode) {
               state.modalMode = modalMode;
          },
     },
     actions: {
          /**
           * Function set isShow Modal
           * @param {*} param0 
           * @param {*} isShow 
           */
          setIsShow({ commit }, isShow) {
               commit("setIsShow", isShow);
          },

          /**
          * Function set Mode Modal
          * @param {*} param0 
          * @param {*} isShow 
          */
          setModalMode({ commit }, modalMode) {
               commit("setModalMode", modalMode);
          }
     },
     getters: {
          /**
           * Get state isShow
           * @param {*} state 
           * @returns 
           */
          isShow(state) {
               return state.isShow;
          },
          /**
           * Get state modalMode
           * @param {*} state 
           * @returns 
           */
          modalMode(state) {
               return state.modalMode
          }
     }

}

export default modalDialog;