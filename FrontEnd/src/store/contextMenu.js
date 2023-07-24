const contextMenu = {
    namespaced: true,
    state: {
        isShowMenu: false,
        pos: { x: 0, y: 0 },
        dataCMenu: null,
    },
    mutations: {
        /**
         * Mutation set state isShow
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} isShow 
         */
        setShowMenu(state, isShow) {
            state.isShowMenu = isShow;
        },

        /**
         * Mutation set state data contextMenu
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} data 
         */
        setDataMenu(state, data) {
            state.dataCMenu = data;
        },

        /**
         * Mutation set state position contextMenu
         * Author: HMDUC(29/05/2023)
         * @param {*} state 
         * @param {*} pos 
         */
        setPos(state, pos) {
            state.pos.x = pos.x;
            state.pos.y = pos.y;
        },
    },
    actions: {
        /**
         * Funtion set show contextMenu
         * Author: HMDUC(29/05/2023)
         * @param {*} param0 
         * @param {*} isShowMenu 
         */
        setShowMenu({ commit }, isShowMenu) {
            commit("setShowMenu", isShowMenu);
        },

        /**
        * Funtion set data contextMenu
        * Author: HMDUC(29/05/2023)
        * @param {*} param0 
        * @param {*} dataCMenu 
        */
        setDataMenu({ commit }, dataCMenu) {
            commit("setDataMenu", dataCMenu);
        },

        /**
        * Funtion set data contextMenu
        * Author: HMDUC(29/05/2023)
        * @param {*} param0 
        * @param {*} dataCMenu 
        */
        setPos({ commit }, pos) {
            commit("setPos", pos);
        },
    },
    getters: {
        /**
         * Get state isShowMenu contextMenu
         * Author: HMDUC(29/05/2023)
         */
        isShowMenu(state) {
            return state.isShowMenu;
        },

        /**
         * Get state data contextMenu
         * Author: HMDUC(29/05/2023)
         */
        dataCMenu(state) {
            return state.data;
        },

        /**
         * Get state position contextMenu
         * Author: HMDUC(29/05/2023)
         */
        pos(state) {
            return state.pos;
        },
    },
};

export default contextMenu;
