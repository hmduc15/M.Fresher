/**
 * funtionn check value is null or empty
 * @param {*} value 
 * @returns true: valid , false: invalid
 */

export function isEmptyOrNull(value) {
     return !(value === null || value === undefined || value === "");
}

export * as Validate from "@/utils/validate"