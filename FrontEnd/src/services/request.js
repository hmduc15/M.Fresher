import axios from "axios";


const request = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
})

/**
 * Funcion call api get data
 * Author: HMDUC (01/06/2023)
 * @param {*} path 
 * @param {*} options 
 * @returns data
 */
export const get = async (path, options) => {
    const res = await request.get(path, options);
    return res.data;
}

/**
 * Function delete asset by id
 * Author: HMDUC (01/06/2023)
 * @param {*} id 
 * @returns respone
 */
export const deleteProperty = async (id) => {
    const res = await request.delete(`/Assets/${id}`);
    return res;
}

/**
 * Function add asset 
 * Author: HMDUC (01/06/2023) 
 * @param {} data 
 * @returns respone
 */
export const postProperty = async (data) => {
    const res = await request.post(`/Assets`, data);
    return res;
}

/**
 * Fuction update Asset
 * Author: HMDUC (01/06/2023)
 * @param {*} data 
 * @returns respone 
 */
export const updateProperty = async (data) => {
    const res = await request.put(`/Assets`, data);
    return res;
}


/**
 * Function delete multi asset by ID
 * Author: HMDUC (01/06/2023)
 * @param {*} dataList 
 * @returns 
 */
export const deleteMultiple = async (dataList) => {
    const res = await request.delete(`Assets/DeleteMultiple`, { data: dataList }, {
        headers: { 'Content-Type': 'application/json' }
    });
    return res;
}

/**
 * Function pagging table
 * Author: HMDUC (01/06/2023)
 * @param {*} pageNumber 
 * @param {*} pageSize 
 * @returns list asset
 */
export const paggingTable = async (pageNumber, pageSize, searchInput, departmentName, categoryName) => {
    let queryParams = `pageSize=${pageSize}&pageNumber=${pageNumber}`;
    if (searchInput) {
        queryParams += `&searchInput=${searchInput}`;
    }
    if (departmentName) {
        queryParams += `&m_DepartmentName=${departmentName}`;
    }
    if (categoryName) {
        queryParams += `&m_CategoryName=${categoryName}`;
    }
    const res = await request.get(`/Assets/PaggingList?${queryParams}`);

    return res.data;
}

/**
 * Function pagging table receipt
 * @param {*} pageNumber 
 * @param {*} pageSize 
 * @param {*} searchInput 
 * @param {*} departmentName 
 * @param {*} categoryName 
 * Author: HMDUC (27/07/2023)
 */
export const paggingTableTranferReceipt = async (pageNumber, pageSize, searchInput, departmentName, categoryName) => {
    let queryParams = `pageSize=${pageSize}&pageNumber=${pageNumber}`;
    if (searchInput) {
        queryParams += `&searchInput=${searchInput}`;
    }
    if (departmentName) {
        queryParams += `&m_DepartmentName=${departmentName}`;
    }
    if (categoryName) {
        queryParams += `&m_CategoryName=${categoryName}`;
    }
    const res = await request.get(`/Receipt/PaggingList?${queryParams}`);

    return res.data;
}

/**
 * Function get and pagging table asset tranfer
 * @param {*} id 
 * @param {*} pageNumber 
 * @param {*} pageSize 
 * Author: HMDUC (27/07/2023) 
 */
export const paggingTableTranferAsset = async (id, pageNumber, pageSize,) => {
    const res = await request.get(`Receipt/PaggingListAsset?id=${id}&pageSize=${pageSize}&pageNumber=${pageNumber}`);
    return res.data;
}
/**
 * Function get and pagging list asset chose
 * @param {*} ids 
 * @param {*} pageNumber 
 * @param {*} pageSize 
 * @returns 
 */
export const paggingAssetChose = async (ids, pageSize, pageNumber) => {
    const res = await request.post(`Assets/PaggingAssetChose`, { ids: ids, pageSize: pageSize, pageNumber: pageNumber });
    return res.data;
}



export * as request from "@/services/request"