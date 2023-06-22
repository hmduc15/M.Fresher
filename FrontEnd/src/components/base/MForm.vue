<template>
  <m-toast
    :isShow="isShowToast"
    :labelField="contentToast"
    type="notice"
  ></m-toast>
  <div class="form__wrapper">
    <form class="form__container" v-esc="closeForm">
      <div class="form__body">
        <m-button
          type="button"
          className="btn__close"
          iconButton="icon__close"
          @click="closeForm"
          title="Đóng"
          posTooltip="right"
        ></m-button>
        <p class="form__title">{{ titleForm }}</p>
        <div class="form__input--container">
          <div class="form--left">
            <div class="input__wrapper">
              <m-input
                ref="property_id"
                type="text"
                className="input__text input--default"
                name="property_id"
                :required="true"
                :isLabel="true"
                :isFocus="true"
                v-model="property.propertyId"
              >
              </m-input>
            </div>
            <div class="input__wrapper">
              <m-combobox
                ref="department_id"
                icon="icon__arrow--xl"
                placeholder="Chọn mã bộ phận sử dụng"
                iconPos="right"
                name="department_id"
                :isFilter="true"
                :listOptions="listDepartment"
                v-model="property.departmentId"
                isLabel="Mã bộ phận sử dụng"
                @change="changeDepartment"
              ></m-combobox>
            </div>
            <div class="input__wrapper">
              <m-combobox
                ref="department_name"
                icon="icon__arrow--xl"
                placeholder="Chọn mã loại tài sản"
                iconPos="right"
                name="department_name"
                :listOptions="listPropertyType"
                :isFilter="true"
                v-model="property.propertyTypeId"
                isLabel="Mã loại tài sản"
                @change="changeType"
              ></m-combobox>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="count"
                type="text"
                v-model="property.propertyQuantity"
                className="input__text input--default text--right"
                name="count"
                :required="true"
                :isLabel="true"
                @input="
                  property.propertyQuantity = this.checkNumber(
                    property.propertyQuantity
                  )
                "
              >
              </m-input>
              <div class="input__btn">
                <m-button
                  type="button"
                  iconButton="icon__up"
                  @click="handleIncrease(this.property.propertyQuantity, 1)"
                ></m-button>
                <m-button
                  type="button"
                  iconButton="icon__down"
                  @click="handleDecrease(this.property.propertyQuantity, 1)"
                ></m-button>
              </div>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="wear_out"
                type="text"
                v-model="property.propertyWorn"
                className="input__text input--default text--right"
                name="wear_out"
                :required="true"
                :isLabel="true"
                @input="
                  property.propertyWorn = this.checkNumber(
                    property.propertyWorn
                  )
                "
              >
              </m-input>
              <div class="input__btn">
                <m-button type="button" iconButton="icon__up"></m-button>
                <m-button type="button" iconButton="icon__down"></m-button>
              </div>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="date_buy"
                :required="true"
                :isLabel="true"
                label="Ngày mua"
                type="date"
                name="date_buy"
                className=" input--default text--left"
                v-model="property.dateBuy"
              ></m-input>
            </div>
          </div>
          <div class="form--right">
            <div class="input__wrapper">
              <m-input
                ref="property_name"
                type="text"
                v-model="property.propertyName"
                className="input__text input--xl"
                name="property_name"
                :required="true"
                :isLabel="true"
              >
              </m-input>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="department_name"
                type="text"
                v-model="property.departmentName"
                className="input__text input--xl"
                name="department_name"
                :required="false"
                :isLabel="true"
              >
              </m-input>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="name_propertyType"
                type="text"
                v-model="property.propertyTypeName"
                className="input__text input--xl"
                name="name_propertyType"
                :required="false"
                :isLabel="true"
              >
              </m-input>
            </div>
            <div class="input__row">
              <div class="input__wrapper">
                <m-input
                  ref="price_default"
                  type="text"
                  v-model="property.propertyPrice"
                  className="input__text input--default text--right"
                  name="price_default"
                  :required="true"
                  :isLabel="true"
                  @input="
                    property.propertyPrice = this.formatInputMoney(
                      property.propertyPrice
                    )
                  "
                >
                </m-input>
              </div>
              <div class="input__wrapper">
                <m-input
                  ref="year_used"
                  type="text"
                  value=""
                  className="input__text input--default text--right"
                  name="year_used"
                  :required="true"
                  :isLabel="true"
                  v-model="property.yearUsed"
                >
                </m-input>
              </div>
            </div>
            <div class="input__row">
              <div class="input__wrapper">
                <m-input
                  ref="price_wornout"
                  type="text"
                  v-model="property.propertyPriced"
                  className="input__text input--default text--right"
                  name="price_wornout"
                  :required="true"
                  :isLabel="true"
                >
                </m-input>
              </div>
              <div class="input__wrapper">
                <m-input
                  ref="year_follow"
                  type="text"
                  value=""
                  className="input__text input--default text--right"
                  name="year_follow"
                  :required="true"
                  :isLabel="true"
                  :isDisabled="true"
                  v-model="this.setYearCurrent"
                >
                </m-input>
              </div>
            </div>
            <div class="input__wrapper">
              <m-input
                ref="date_used"
                type="date"
                className=" input--default text--left"
                v-model="property.dateUsed"
                name="date_used"
                :required="true"
                :isLabel="true"
              ></m-input>
            </div>
          </div>
        </div>
      </div>
      <div class="form__footer">
        <div class="form__button">
          <m-button
            @click="closeForm"
            type="button"
            content="Hủy"
            className="btn__sub btn__cancel"
          ></m-button>
          <m-button
            type="button"
            content="Lưu"
            className="btn__main btn__submit"
            @click="submitData"
          ></m-button>
        </div>
      </div>
    </form>
  </div>
</template>
<script>
import { format } from "@/utils/format";
import Enum from "@/utils/enum";

import MInput from "./MInput.vue";
import MButton from "./MButton.vue";
import MToast from "./MToast.vue";
import MCombobox from "@/components/base/MCombobox";

import { mapActions, mapState } from "vuex";

export default {
  name: "MForm",
  components: {
    "m-input": MInput,
    "m-button": MButton,
    "m-toast": MToast,
    "m-combobox": MCombobox,
  },
  props: ["title", "data", "id"],
  data() {
    return {
      property: {
        propertyId: "",
        propertyName: "",
        propertyTypeName: "",
        propertyTypeId: "",
        propertyQuantity: "",
        propertyPrice: "",
        propertyWorn: "",
        propertyPriced: "",
        departmentName: "",
        departmentId: "",
        yearUsed: "",
        yearFollow: "",
        dateUsed: "",
        dateBuy: "",
      },
      isError: false,
      listDepartment: [
        {
          label: "BP001",
          value: "Phòng Kĩ thuật",
        },
        {
          label: "BP002",
          value: "Phòng Hành chính Kế Toán",
        },
        {
          label: "BP003",
          value: "Phòng Thư ký",
        },
      ],
      listPropertyType: [
        {
          label: "TS001",
          value: "Máy tính xách tay",
        },
        {
          label: "TS002",
          value: "Máy tính để bàn",
        },
        {
          label: "TS003",
          value: "Điện thoại",
        },
      ],
      selectedOption: null,
    };
  },

  computed: {
    ...mapState("toastMessage", ["isShowToast", "contentToast"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("yearSelected", ["yearSelected"]),

    setYearCurrent() {
      return this.yearSelected;
    },
    titleForm() {
      return this.data
        ? this.$_MISAResources.form__title["EditTitle"]
        : this.$_MISAResources.form__title["AddTitle"];
    },
    isValid() {
      for (const inputRef in this.$refs) {
        if (this.$refs[inputRef].required && this.$refs[inputRef].validate()) {
          return false;
        }
      }
      return true;
    },
  },

  mounted() {
    if (this.data) {
      this.property = this.data;
    }
  },
  methods: {
    ...mapActions("formDialog", ["setIsShow"]),
    ...mapActions("property", ["postProperty"]),

    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj contain id and name department
     *
     */
    changeDepartment(obj) {
      this.selectedOption = obj.label;
      const department = this.findOptionById(
        this.listDepartment,
        this.selectedOption
      );
      this.property.departmentName = department.value;
    },
    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj
     *
     */
    changeType(obj) {
      this.selectedOption = obj.label;
      const department = this.findOptionById(
        this.listPropertyType,
        this.selectedOption
      );
      this.property.propertyTypeName = department.value;
    },
    /**
     * function find option selected by id when select departmentID or propertyID
     * Author: HMDUC (01/06/2023)
     * @param {*} arr
     * @param {*} opt
     * @return Id: BP001 -> Phòng kỹ thuật || TS001 -> Máy tính xách tay
     */
    findOptionById(arr, opt) {
      return arr.find((item) => {
        return item.label == opt;
      });
    },
    /**
     * function format money
     * Author: HMDUC (04/06/2023)
     * @param {*} value
     * return 1.000.000
     */
    formatInputMoney(value) {
      //remove character not number
      let money = this.checkNumber(value);

      //check invalid money input
      if (/^\d+$/.test(money)) {
        let formatted = format.formatMoney(money);
        return formatted;
      }
    },
    /**
     * function require value input is number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return number: 9999
     */
    checkNumber(value) {
      return value.replace(/\D/g, "");
    },
    /**
     * function handle increase field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value + 1
     */
    handleIncrease(value, number) {
      this.property.propertyQuantity = parseInt(value) + number;
    },
    /**
     * function handle decrease field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value - 1
     */
    handleDecrease(value, number) {
      this.property.propertyQuantity = parseInt(value) - number;
    },
    /**
     * function close form
     * Author: HMDUC (28/05/2023)
     */
    closeForm() {
      this.setIsShow(false);
    },
    /**
     * function submit form
     * Author: HMDUC (28/05/2023)
     */
    submitData() {
      let mode = this.formMode;
      if (this.isValid) {
        switch (mode) {
          case Enum.FORM__MODE.ADD:
            //add property
            this.postProperty(this.property);
            this.closeForm();
            break;
          case Enum.FORM__MODE.EDIT:
            //add property

            break;
          default:
            break;
        }
      } else {
        //hanlde invalid
        console.log("invalid");
      }
    },
  },
};
</script>
<style>
@import "@/css/base/form.css";
</style>
