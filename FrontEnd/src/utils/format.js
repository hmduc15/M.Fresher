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