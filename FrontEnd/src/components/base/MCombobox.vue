<template>
  <label v-if="isLabel" class="label__input">
    {{ isLabel }}
  </label>
  <div
    class="select__icon"
    :name="name"
    :class="[
      icon,
      `${iconPos === 'left' ? 'select__icon--l' : 'select__icon--r'}`,
    ]"
  ></div>
  <m-select
    ref="selectRef"
    :model-value="modelValue"
    :class="['select__wrapper', { 'select--focus': isFocus }]"
    :placeholder="placeholder"
    @change="handleChangeOpt"
    @update:model-value="handleDate"
    :filterable="isFilter"
    autocomplete="Không có dữ liệu"
    @focus="handleFocus"
    @blur="handleBlur"
  >
    <m-option
      class="select__option"
      v-for="item in listOptions"
      :key="item.value"
      :value="item.label"
      :label="item.label"
      @updateOptions="handleUpdateOpt"
    ></m-option>
  </m-select>
</template>

<script>
import { ElSelect } from "element-plus";
import { ElOption } from "element-plus";
import { ref } from "vue";

export default {
  name: "MCombobox",
  props: [
    "listOptions",
    "icon",
    "placeholder",
    "isLabel",
    "iconPos",
    "modelValue",
    "name",
    "isFilter",
  ],
  data() {
    return {
      isFocus: false,
    };
  },
  setup() {
    const value = ref("");
    return {
      value,
    };
  },
  components: {
    "m-select": ElSelect,
    "m-option": ElOption,
  },
  emits: ["change", "update:modelValue", "updateOptions"],

  methods: {
    handleFocus() {
      this.isFocus = true;
    },
    handleBlur() {
      this.isFocus = false;
    },

    handleChangeOpt(value) {
      this.updateModelValue(value);
      this.handleUpdateOpt(value);
      this.$emit("change", value);
    },
    /**
     * function emit date when select
     * @param {*} date
     * Author: HMDUC (01/06/2023)
     */
    handleDate(date) {
      this.updateModelValue(date);
    },
    /**
     * function update localModelValue
     * @param {*} date
     * Author: HMDUC (01/06/2023)
     */
    updateModelValue(value) {
      this.$emit("update:modelValue", value);
    },

    handleUpdateOpt(option) {
      this.$emit("updateOptions", option);
    },
  },
};
</script>

<style></style>
