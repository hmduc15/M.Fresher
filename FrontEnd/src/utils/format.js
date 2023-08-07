/**
 * function format money
 * Author: HMDUC (04/06/2023)
 * @param {*} number
 * @returns 1.000.000
 */
export function formatMoney(number) {
    let amount = Number(number);
    let formatted = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return formatted;
}

/**
 * funtion format decimal
 * Author: HMDUC (04/06/2023)
 * @param {*} number 
 * @returns 2.33 -> 2,33
 */
export function formatDenary(number) {
    let formatted = number.toString().replace(".", ",");
    return formatted;
}

/**
 * Funtion format date
 * @param {*} data 
 * @returns 22/02/2023
 */
export function formatDate(data) {
    let date = new Date(data);
    let year = date.getFullYear().toString();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');

    return `${day}/${month}/${year}`;

}

/**
 * Function formatIndo China Time
 * @param {*} date 
 * @returns 
 */
export function formatIndochinaTime(date) {
    const gmtOffset = -7 * 60;
    const time = new Date(date);
    const indoTime = new Date(time.getTime() + gmtOffset * 60 * 1000);
    return indoTime;
}

/**
 * Function format time
 * @param {*} date 
 * @returns 
 */
export function convertRequest(date) {
    const time = new Date(date);
    const year = time.getFullYear();
    const month = time.getMonth() + 1; // Lưu ý: getMonth() trả về giá trị từ 0 đến 11
    const day = time.getDate();
    const hours = time.getHours();
    const minutes = time.getMinutes();
    const seconds = time.getSeconds();
    const formatDate = `${year}-${month.toString().padStart(2, "0")}-${day.toString().padStart(2, "0")}T${hours.toString().padStart(2, "0")}:${minutes.toString().padStart(2, "0")}:${seconds.toString().padStart(2, "0")}.000`;
    return formatDate
}

/**
 * Function format to ISOstring
 * Author: HMDUC (15/12/2002)
 */

export function convertToISOString(date) {
    const parts = date.split("/");
    const day = parts[0];
    const month = parts[1];
    const year = parts[2];
    const dateObject = new Date(`${year}-${month}-${day}`);
    const isoString = dateObject.toISOString();
    return isoString;

}

/**
 * funtion format decimal
 * Author: HMDUC (04/06/2023)
 * @param {*} number 
 * @returns 2,33
 */
export function formaDecimal(number) {
    let amount = Number(number);
    let formatted = amount.toString().replace(/\B(?=(\d{2})+(?!\d))/g, ',');
    return formatted;
}


export * as format from "@/utils/format"