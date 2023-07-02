/**
 * function formate number
 * Author: HMDUC (04/06/2023)
 * @param {*} number
 * @returns 1.000.000
 */
export function formatMoney(number) {
    let amount = Number(number);
    let formatted = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return formatted;
}



export function formatFloat(number) {
    let formatted = number.toString().replace(".", ",");
    return formatted;
}

export function unformatFloat(number) {
    let formatted = parseFloat(number.replace(",", "."));
    return formatted;
}



export * as format from "@/utils/format"