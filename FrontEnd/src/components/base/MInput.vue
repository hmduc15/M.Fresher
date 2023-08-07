<!-- eslint-disable vue/no-mutating-props -->
<template>
  <div class="input">
    <label v-if="isLabel" class="label__input" :for="id" @click="handleFocus">
      {{ this.$_MISAResources.label__input[name] }}
      <span v-if="required"> * </span>
    </label>
    <!-- Input Checkbox -->
    <template v-if="type === 'checkbox'">
      <!-- Checkbox input template -->
      <input
        :type="type"
        :class="className"
        :placeholder="placeHolder"
        :id="id"
        :name="name"
        :value="modelValue"
        :disabled="isDisabled"
        @input="handleInput($event)"
      />
    </template>
    <!-- Input Text -->
    <template v-else-if="type === 'text'">
      <!-- Text input template -->
      <input
        ref="inputRef"
        :type="type"
        :class="[
          className,
          requiredNot ? 'input--white' : '',
          required ? '' : 'input--gray',
          number ? 'input__number--r' : '',
          isError ? 'input--error' : '',
          isBorder ? 'input--border-focus' : '',
        ]"
        :placeholder="placeHolder"
        :id="id"
        :name="name"
        :value="modelValue"
        :disabled="isDisabled"
        @input="handleInput($event)"
        @blur="required ? validate() : ''"
        @focus="handleFocus"
        :maxlength="maxLength"
        :isFocus="isFocus"
        :tabindex="tabIndex"
        autocomplete="off"
      />
      <div v-show="isError" class="input__text--err">
        {{ this.errMesage }}
      </div>
    </template>
    <!-- Input Datepicker -->
    <template v-else-if="type === 'date'">
      <VueDatePicker
        v-model="modelValue"
        :format="dateFormat"
        :name="name"
        :day-names="this.$_MISAResources.listDay"
        locale="vi"
        @update:model-value="handleDate"
        text-input
        auto-apply
        :enable-time-picker="false"
        clearable
        @clickDate="log"
        @focus="focusDate"
      >
        <template #input-icon>
          <div class="date__input_icon icon__date"></div
        ></template>
        <template #dp-input="{ value, onEnter, onBlur, onFocus }">
          <input
            ref="inputRef"
            :class="[
              className,

              required ? '' : 'input--gray',
              number ? 'input__number--r' : '',
              isError ? 'input--error' : '',
            ]"
            type="text"
            :value="value"
            :isFocus="isFocus"
            @blur="onBlur"
            @keydown.enter="onEnter"
            @focus="onFocus"
            @click="handeleMoveError"
          />
        </template>
      </VueDatePicker>
      <div v-show="isError" class="input__text--err">
        {{ this.errMesage }}
      </div>
    </template>
  </div>
</template>

<script>
import Enum from "@/utils/enum";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { Validate } from "@/utils/validate";
import { mapActions, mapState } from "vuex";

export default {
  name: "MInput",
  components: {
    VueDatePicker,
  },
  props: [
    "tabIndex",
    "number",
    "type",
    "className",
    "placeHolder",
    "id",
    "name",
    "textLabel",
    "icon",
    "labelName",
    "modelValue",
    "required",
    "isCheck",
    "isLabel",
    "isDisabled",
    "isFocus",
    "maxLength",
    "requiredNot",
    "isNo",
  ],
  emits: ["update:modelValue", "clickDate"],

  data() {
    return {
      dateVal: new Date(),
      dateFormat: Enum.FORMAT__DATE.VI,
      isError: false,
      labelError: null,
      errMesage: null,
      isBorder: false,
    };
  },

  computed: {
    ...mapState("toastMessage", ["isShowToast", "contentToast"]),
  },

  watch: {
    modelValue(newValue) {
      this.updateModelValue(newValue);
    },
  },

  updated() {
    if (this.type === "date" && !this.modelValue) {
      this.updateModelValue(this.dateVal);
    }
  },

  created() {
    if (this.type === "date" && !this.modelValue) {
      this.updateModelValue(this.dateVal);
    }
  },

  mounted() {
    this.focusFirstInput();
  },

  methods: {
    ...mapActions("toastMessage", ["setIsShowToast", "setContentToast"]),
    handeleMoveError() {
      if (this.isError) {
        this.isError = false;
      }
    },
    onFocus() {
      if (this.isError) {
        this.isError = false;
      }
    },

    handleChecked(value) {
      this.updateModelValue(value);
    },

    /**
     * Function handle event focus
     * Author: HMDUC (29/05/2023)
     */
    handleFocus() {
      this.$refs.inputRef.focus();
      this.$refs.inputRef.select();
    },

    /**
     * Funtion handle focus first input
     * Author: HMDUC (29/05/2023)
     */
    focusFirstInput() {
      if (this.isFocus) {
        this.$nextTick(() => {
          this.$refs.inputRef.focus();
          this.$refs.inputRef.select();
        });
      }
    },

    /**
     * Function handle input value
     * Author: HMDUC (29/05/2023)
     */
    handleInput(e) {
      this.$emit("update:modelValue", e.target.value);
      if (e.target.value) {
        this.isError = false;
      }
      if (this.type === "date") {
        this.isError = false;
      }
    },

    /**
     * Function handle input type date
     * Author: HMDUC (29/05/2023)
     */
    handleDate(date) {
      this.updateModelValue(date);
    },

    /**
     * Function update model value
     * Author: HMDUC (29/05/2023)
     * @param {*} value
     */
    updateModelValue(value) {
      this.$emit("update:modelValue", value);
    },

    /**
     * Function check validate input
     * Author: HMDUC (29/05/2023)
     */
    validate() {
      const self = this;
      const value = self.modelValue;
      const nameLabel = this.$_MISAResources.label__input[self.name];
      if (!Validate.isEmptyOrNull(value)) {
        self.isError = true;
        self.errMesage =
          nameLabel + " " + this.$_MISAResources.text__error.inputErr;
        return self.isError;
      } else {
        self.isError = false;
        return self.isError;
      }
    },
  },
};
</script>

<style>
@import "@/css/base/input.css";
</style>
