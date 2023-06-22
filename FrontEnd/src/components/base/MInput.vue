<!-- eslint-disable vue/no-mutating-props -->
<template>
  <div class="input">
    <label v-if="isLabel" class="label__input" :for="id">
      {{ this.$_MISAResources.label__input[name] }}
      <span v-if="required"> * </span>
    </label>
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
      />
    </template>
    <template v-else-if="type === 'text'">
      <!-- Text input template -->
      <input
        ref="inputRef"
        :type="type"
        :class="[
          className,
          required ? '' : 'input--gray',
          number ? 'input__number--r' : '',
          isError ? 'input--error' : '',
        ]"
        :placeholder="placeHolder"
        :id="id"
        :name="name"
        :value="modelValue"
        :disabled="isDisabled"
        @input="handleInput($event)"
        @blur="required ? validate() : ''"
        :maxlength="maxLength"
      />
      <div v-show="isError" class="input__text--err">
        {{ this.labelError }}
        {{ this.$_MISAResources.text__error["input_err"] }}
      </div>
    </template>
    <template v-else-if="type === 'date'">
      <!-- Date input template with default value -->
      <VueDatePicker
        v-model="modelValue"
        :class="className"
        :format="dateFormat"
        :name="name"
        @update:model-value="handleDate"
        auto-apply
      >
      </VueDatePicker>
      <div class="date__input_icon icon__date"></div>
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
  ],
  emits: ["update:modelValue"],

  data() {
    return {
      dateVal: new Date(),
      dateFormat: Enum.FORMAT__DATE.VI,
      isError: false,
      labelError: null,
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

  methods: {
    ...mapActions("toastMessage", ["setIsShowToast", "setContentToast"]),

    handleInput(e) {
      this.$emit("update:modelValue", e.target.value);
      if (e.target.value) {
        this.isError = false;
      }
    },

    handleDate(date) {
      this.updateModelValue(date);
    },

    updateModelValue(value) {
      this.$emit("update:modelValue", value);
    },

    validate() {
      const self = this;
      const value = self.modelValue;
      const nameLabel = this.$_MISAResources.label__input[self.name];
      if (!Validate.isEmtyOrNull(value)) {
        self.isError = true;
        self.labelError = nameLabel;
        this.$nextTick(() => {
          this.$refs.inputRef?.focus();
        });
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
