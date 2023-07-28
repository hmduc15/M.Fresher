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