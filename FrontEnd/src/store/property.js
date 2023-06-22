import { request } from "@/services/request";

const property = {
    namespaced: true,
    state: {
        loadingTime: null,
        isLoading: false,
        propertyList: [],
        listSelected: [],
    },
    mutations: {
        setLoading(state, isLoading) {
            state.isLoading = isLoading
        },
        setLoadingTime(state, loadingTime) {
            state.loadingTime = loadingTime;
        },
        setPropertyList(state, propertyList) {
            state.propertyList = propertyList;
        },
        setListSelected(state, listSelected) {
            state.listSelected = listSelected;
        }

    },
    actions: {
        /**
         * function setloading, call api
         * Author: HMDUC(29/05/2023)
         */
        async getPropertyList({ commit }, { pageNumber = 1, pageSize = 20, searchInput = null, departmentName = null, categoryName = null } = {}) {
            const startTime = new Date().getTime();
            let responseTime = 0;
            commit("setLoading", true);
            try {
                const res = await request.paggingTable(pageNumber, pageSize, searchInput, departmentName, categoryName);
                const endTime = new Date().getTime();
                responseTime = endTime - startTime;
                commit("setLoadingTime", responseTime);
                commit("setPropertyList", res);
            } catch (err) {
                console.log(err);
            } finally {
                setTimeout(() => {
                    commit("setLoading", false);
                }, responseTime + 1000);
            }
        },
        /**
        * function delete property by id
        * Author: HMDUC(29/05/2023)
        */
        async delete({ dispatch }, id) {
            try {
                const res = await request.deleteProperty(id);
                //update data after delete
                dispatch("getPropertyList");
                return res.status;
            } catch (err) {
                return err.response.status;
            }
        },

        async deleteMulti({ dispatch }, listId) {
            try {
                const res = await request.deleteMultiple(listId);
                dispatch("getPropertyList");
                return res.status;
            } catch (err) {
                return err.response.status;
            }
        },

        async postProperty({ dispatch }, data) {
            try {
                const res = await request.postProperty(data);
                //update data after add property
                dispatch("getPropertyList");
                return res;
            } catch (err) {
                return err.response;
            }
        },

        async updateProperty({ dispatch }, data) {
            try {
                const res = await request.updateProperty(data);
                //update data after update property
                dispatch("getPropertyList");
                return res;
            } catch (err) {
                return err.response;
            }
        },

        setListSelected({ commit }, listSelected) {
            commit("setListSelected", listSelected);
        }

    },
    getters: {
        loadingTime(state) {
            return state.loadingTime;
        },
        propertyList(state) {
            return state.propertyList;
        },
        listSelected(state) {
            return state.listSelected;
        },
        isLoading(state) {
            return state.isLoading;
        }
    }
}

export default property;