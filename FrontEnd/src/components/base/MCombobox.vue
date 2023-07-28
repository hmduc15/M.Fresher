<template>
  <label v-if="isLabel" class="label__input">
    {{ isLabel }}
    <span v-if="required"> * </span>
  </label>
  <m-select
    ref="selectRef"
    :model-value="modelValue"
    :class="[
      'select__wrapper',
      { 'select--focus': isFocus, 'input--error': isError },
    ]"
    :placeholder="placeholder"
    :name="name"
    @change="handleChangeOpt"
    @update:model-value="handleDate"
    :filterable="isFilter"
    @focus="handleFocus"
    @blur="required ? validate() : handleBlur()"
    :tabindex="tabIndex"
    :placement="placement"
  >
    <template v-slot:prefix>
      <div
        class="select__icon"
        :name="name"
        :class="[
          iconPrefix,
          `${iconPos === 'left' ? 'select__icon--l' : 'select__icon--r'}`,
        ]"
      ></div>
    </template>
    <template v-slot:empty>
      <div class="text--empty">
        {{ this.$_MISAResources.combobox.EmptyOpt }}
      </div>
    </template>
    <m-option
      class="select__option"
      v-for="item in listOptions"
      :key="item.value"
      :value="item.value"
      :label="item.label"
      @updateOptions="handleUpdateOpt"
    ></m-option>
  </m-select>
  <div v-show="isError" class="input__text--err">
    {{ this.errMesage }}
  </div>
</template>

<script>
import { Validate } from "@/utils/validate";

import { ElSelect } from "element-plus";
import { ElOption } from "element-plus";
import { ref } from "vue";

export default {
  name: "MCombobox",
  props: [
    "tabIndex",
    "listOptions",
    "iconPrefix",
    "iconSubfix",
    "placeholder",
    "isLabel",
    "iconPos",
    "modelValue",
    "name",
    "isFilter",
    "required",
    "placement",
  ],
  data() {
    return {
      isFocus: false,
      isError: false,
      labelError: null,
      errMesage: null,
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
    /**
     * function handle Focus Combobox
     * Author: HMDUC (01/06/2023)
     */
    handleFocus() {
      this.isFocus = true;
      this.isError = false;
    },

    /**
     * function handle Blur Combobox
     * Author: HMDUC (01/06/2023)
     */
    handleBlur() {
      this.isFocus = false;
    },

    /**
     * function handle change option
     * @param {*} value
     * Author: HMDUC (01/06/2023)
     */
    handleChangeOpt(value) {
      this.isFocus = false;
      if (value) {
        this.isError = false;
      }
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

    /**
     * Funtion update option when change
     * @param {*} option
     * Author: HMDUC (01/06/2023)
     */
    handleUpdateOpt(option) {
      this.$emit("updateOptions", option);
    },

    /**
     * funtion validate combobox
     * Author: HMDUC (01/06/2023)
     */
    validate() {
      this.isFocus = !this.isFocus;
      const self = this;
      const value = self.modelValue;
      const nameLabel = this.$_MISAResources.label__input[self.name];
      self.errMesage =
        nameLabel + " " + this.$_MISAResources.text__error.inputErr;

      if (!Validate.isEmptyOrNull(value)) {
        self.isError = true;
        return self.isError;
      }
    },
  },
};
</script>

<style></style>
