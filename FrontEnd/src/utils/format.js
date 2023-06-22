/**
 * function formate money
 * Author: HMDUC (04/06/2023)
 * @param {*} money 
 * @returns 1.000.000
 */
export function formatMoney(money) {
    let amount = Number(money);
    let formatted = amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return formatted;
}





export * as format from "@/utils/format"