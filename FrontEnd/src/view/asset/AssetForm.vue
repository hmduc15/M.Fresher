<template>
  <m-dialog v-if="isShowForm">
    <template #content>
      <div class="form__wrapper">
        <form
          class="form__container"
          v-esc="closeForm"
          v-ctrl-w="handleCancel"
          v-ctrl-s="submitData"
        >
          <div class="form__body">
            <m-button
              type="button"
              className="btn__close"
              iconButton="icon__close"
              @click="handleCancel"
              :title="this.$_MISAResources.tooltip__btn.close"
              posTooltip="right"
            ></m-button>
            <p class="form__title">{{ titleForm }}</p>
            <div class="form__input--container">
              <div class="form--row">
                <div class="input__wrapper">
                  <m-input
                    ref="AssetCode"
                    type="text"
                    className="input__text input--default"
                    name="AssetCode"
                    :required="true"
                    :isLabel="true"
                    :isFocus="true"
                    :placeHolder="this.$_MISAResources.placeholder.AssetCode"
                    :maxLength="100"
                    v-model="asset.AssetCode"
                    @keydown.shift.prevent.tab="moveFocus"
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="AssetName"
                    type="text"
                    v-model="asset.AssetName"
                    className="input__text input--xl"
                    name="AssetName"
                    :required="true"
                    :maxLength="255"
                    :isLabel="true"
                    :placeHolder="this.$_MISAResources.placeholder.AssetName"
                  >
                  </m-input>
                </div>
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-combobox
                    ref="DepartmentCode"
                    iconPrefix="icon__arrow--xl"
                    placeholder="Chọn mã bộ phận sử dụng"
                    iconPos="right"
                    name="DepartmentCode"
                    :isFilter="true"
                    :required="true"
                    :listOptions="listDepartment"
                    v-model="asset.DepartmentCode"
                    isLabel="Mã bộ phận sử dụng"
                    @change="changeDepartment"
                  ></m-combobox>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="DepartmentName"
                    type="text"
                    v-model="asset.DepartmentName"
                    className="input__text input--xl"
                    name="DepartmentName"
                    :required="false"
                    :isLabel="true"
                    :isDisabled="true"
                  >
                  </m-input>
                </div>
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-combobox
                    ref="CategoryCode"
                    iconPrefix="icon__arrow--xl"
                    placeholder="Chọn mã loại tài sản"
                    iconPos="right"
                    name="CategoryCode"
                    :listOptions="listAssetCategory"
                    :isFilter="true"
                    :required="true"
                    v-model="asset.CategoryCode"
                    isLabel="Mã loại tài sản"
                    @change="changeCategory"
                  ></m-combobox>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="CategoryName"
                    type="text"
                    v-model="asset.CategoryName"
                    className="input__text input--xl"
                    name="CategoryName"
                    :required="false"
                    :isLabel="true"
                    :isDisabled="true"
                  >
                  </m-input>
                </div>
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-input
                    ref="Quantity"
                    type="text"
                    v-model="asset.Quantity"
                    className="input__text input--default text--right"
                    name="Quantity"
                    :number="true"
                    :required="true"
                    :isLabel="true"
                    :maxLength="18"
                    @input="asset.Quantity = this.checkNumber(asset.Quantity)"
                  >
                  </m-input>
                  <div class="input__btn">
                    <div class="btn--increase icon__up"></div>
                    <div class="btn--decrease icon__down"></div>
                  </div>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="Cost"
                    type="text"
                    v-model="asset.Cost"
                    className="input__text input--default text--right"
                    name="Cost"
                    :required="true"
                    :maxLength="18"
                    :isLabel="true"
                    @input="asset.Cost = this.formatInputMoney(asset.Cost)"
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="LifeTime"
                    type="text"
                    className="input__text input--default text--right"
                    name="LifeTime"
                    :required="true"
                    :isLabel="true"
                    v-model="asset.LifeTime"
                  >
                  </m-input>
                </div>
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-input
                    ref="DepreciationRate"
                    type="text"
                    v-model="asset.DepreciationRate"
                    className="input__text input--default text--right"
                    name="DepreciationRate"
                    :number="true"
                    :required="true"
                    :isLabel="true"
                    :maxLength="5"
                    @input="
                      asset.DepreciationRate = this.formatInputDecimal(
                        asset.DepreciationRate
                      )
                    "
                  >
                  </m-input>
                  <div class="input__btn">
                    <div class="btn--increase icon__up"></div>
                    <div class="btn--decrease icon__down"></div>
                  </div>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="DepreciationYear"
                    type="text"
                    v-model="asset.DepreciationYear"
                    className="input__text input--default text--right"
                    name="DepreciationYear"
                    :required="true"
                    :isLabel="true"
                    :maxLength="18"
                    @input="
                      asset.DepreciationYear = this.formatInputMoney(
                        asset.DepreciationYear
                      )
                    "
                  >
                  </m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="TrackedYear"
                    type="text"
                    value=""
                    className="input__text input--default text--right"
                    name="TrackedYear"
                    :required="true"
                    :isLabel="true"
                    :isDisabled="true"
                    v-model="this.setYearCurrent"
                  >
                  </m-input>
                </div>
              </div>
              <div class="form--row">
                <div class="input__wrapper">
                  <m-input
                    ref="PurchaseDate"
                    :required="true"
                    :isLabel="true"
                    className="input__text input--default"
                    type="date"
                    name="PurchaseDate"
                    v-model="asset.PurchaseDate"
                  ></m-input>
                </div>
                <div class="input__wrapper">
                  <m-input
                    ref="ProductionYear"
                    type="date"
                    className="input__text input--default"
                    v-model="asset.ProductionYear"
                    name="ProductionYear"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
              </div>
            </div>
          </div>
          <div class="form__footer">
            <m-button
              type="button"
              :content="this.$_MISAResources.content__button.save"
              className="btn__main btn__submit"
              @click="submitData"
            ></m-button>
            <m-button
              ref="lastBtn"
              type="button"
              :content="this.$_MISAResources.content__button.cancelForm"
              className="btn__sub btn--form "
              @click="handleCancel"
              @keydown.tab.prevent="backTabIndex"
            ></m-button>
          </div>
        </form>
      </div>
    </template>
  </m-dialog>
</template>
<script>
import { mapActions, mapState } from "vuex";
import Enum from "@/utils/enum";
import { format } from "@/utils/format";
import { request } from "@/services/request";
import { Validate } from "@/utils/validate";

import MDiaglog from "@/components/base/MDiaglog.vue";
import MInput from "@/components/base/MInput.vue";
import MButton from "@/components/base/MButton.vue";
import MCombobox from "@/components/base/MCombobox";

export default {
  name: "AssetForm",
  props: ["title", "isShow", "data", "newCode"],
  components: {
    "m-dialog": MDiaglog,
    "m-input": MInput,
    "m-button": MButton,
    "m-combobox": MCombobox,
  },

  data() {
    return {
      dataRow: this.data,
      isShowForm: this.isShow,
      asset: {
        AssetId: "",
        AssetCode: "",
        AssetName: "",
        DepartmentId: "",
        DepartmentCode: "",
        DepartmentName: "",
        CategoryId: "",
        CategoryCode: "",
        CategoryName: "",
        PurchaseDate: "",
        Cost: "",
        Quantity: "",
        DepreciationRate: "",
        DepreciationYear: "",
        TrackedYear: "",
        LifeTime: "",
        ProductionYear: "",
        CreatedBy: "",
        CreatedDate: "",
        ModifiedBy: "",
        ModifiedDate: "",
      },
      assetJson: null,
      isCheckValidate: false,
      listDepartment: null,
      listAssetCategory: null,
      selectedOption: null,
      inputRefs: [],
    };
  },

  created() {
    this.getDepartmentList();
    this.getCategory();
  },

  updated() {
    if (this.data) {
      let objJson = JSON.stringify(this.data);
      this.assetJson = objJson;
      this.asset = JSON.parse(objJson);
      this.asset.DepreciationRate = format.formatDenary(
        parseFloat(this.asset.DepreciationRate * 100).toFixed(2)
      );
    } else {
      this.asset = {};
      this.asset.Quantity = 1;
      this.asset.Cost = 0;
      this.asset.DepreciationYear = 0;
      this.asset.AssetCode = this.newCode;
    }
    this.isShowForm = this.isShow;
  },

  computed: {
    ...mapState("toastMessage", ["isShowToast", "contentToast"]),
    ...mapState("formDialog", ["formMode"]),
    ...mapState("yearSelected", ["yearSelected"]),
    ...mapState("inputError", ["listError"]),

    /**
     * Function set yearSelected
     * Author: HMDUC (20/5/2023)
     */
    setYearCurrent() {
      return this.yearSelected;
    },

    /**
     * Function set title form by formMode
     * Author: HMDUC (20/5/2023)
     */
    titleForm() {
      if (this.formMode === Enum.FORM__MODE.ADD) {
        return this.$_MISAResources.form__title["AddTitle"];
      } else if (this.formMode === Enum.FORM__MODE.DUPLICATE) {
        return this.$_MISAResources.form__title["DuplicateTitle"];
      } else {
        return this.$_MISAResources.form__title["EditTitle"];
      }
    },
  },

  methods: {
    ...mapActions("formDialog", ["setIsShow"]),
    ...mapActions("asset", ["postAsset", "updateAsset"]),
    ...mapActions("inputError", ["setListError"]),
    /**
     * Function move focus when shift+tab
     * Author: HMDUC (19/07/2023)
     */
    moveFocus() {
      this.$refs.lastBtn.$el.focus();
    },

    /**
     * Function set back TabIndex first input
     * Author: HMDUC (17/07/2023)
     */
    backTabIndex() {
      for (var inputRef in this.$refs) {
        this.$nextTick(() => {
          this.$refs[inputRef].handleFocus();
        });
        break;
      }
    },

    /**
     * Function call Api get Department list
     * Author: HMDUC (15/06/2023)
     */
    async getDepartmentList() {
      try {
        const res = await request.get(`/Departments`);
        const mapData = res.map((item) => ({
          id: item.DepartmentId,
          label: item.DepartmentCode,
          value: item.DepartmentName,
        }));
        this.listDepartment = mapData;
      } catch (err) {
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
      }
    },

    /**
     * Function call Api Category list
     * Author: HMDUC (15/06/2023)
     */
    async getCategory() {
      try {
        const res = await request.get(`/Category`);
        const mapData = res.map((item) => ({
          id: item.CategoryId,
          label: item.CategoryCode,
          value: item.CategoryName,
          lifeTime: item.LifeTime,
          depreciationRate: item.DepreciationRate,
        }));
        this.listAssetCategory = mapData;
      } catch (err) {
        this.$emit(
          "showToast",
          "notice",
          this.$_MISAResources.toast__content.ErrorServer
        );
      }
    },

    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj contain id and name department
     *
     */
    changeDepartment(obj) {
      this.selectedOption = obj;
      const department = this.findOptionById(
        this.listDepartment,
        this.selectedOption
      );
      this.asset.DepartmentId = department.id;
      this.asset.DepartmentName = department.value;
    },

    /**
     * function handle select option
     * Author: HMDUC (01/06/2023)
     * @param {*} obj
     *
     */
    changeCategory(obj) {
      this.selectedOption = obj;
      const category = this.findOptionById(
        this.listAssetCategory,
        this.selectedOption
      );
      this.asset.CategoryId = category.id;
      this.asset.CategoryName = category.value;
      this.asset.LifeTime = category.lifeTime;
      this.asset.DepreciationRate = format.formatDenary(
        parseFloat(category.depreciationRate * 100).toFixed(2)
      );
      this.$refs["DepreciationRate"].isError = false;
      this.$refs["LifeTime"].isError = false;

      //Calculate DepreciationYear when change category
      var cost =
        this.asset.Cost === 0 ? 0 : Number(this.asset.Cost.replace(/\./g, ""));
      var DepreciationYear =
        cost *
        (parseFloat(this.asset.DepreciationRate.replace(",", ".")) / 100);

      this.asset.DepreciationYear = format.formatMoney(
        Math.round(DepreciationYear)
      );
    },

    /**
     * function find option selected by id when select departmentID or  categoryID
     * Author: HMDUC (01/06/2023)
     * @param {*} arr
     * @param {*} opt
     * @return Id: BP001 -> Phòng kỹ thuật || TS001 -> Máy tính xách tay
     */
    findOptionById(arr, opt) {
      return arr.find((item) => {
        return item.value == opt;
      });
    },

    /**
     * function format money
     * Author: HMDUC (04/06/2023)
     * @param {*} value
     * @return 1.000.000
     */
    formatInputMoney(value) {
      //remove character not numbe
      let money = this.checkNumber(value);

      if (this.asset.DepreciationRate && money) {
        var DepreciationYear =
          (parseFloat(money).toFixed(3) *
            parseFloat(this.asset.DepreciationRate?.replace(",", "."))) /
          100;
        this.asset.DepreciationYear = format.formatMoney(
          Math.round(DepreciationYear)
        );
      }

      //check invalid money input
      if (/^\d+$/.test(money)) {
        let formatted = format.formatMoney(money);
        return formatted;
      }
    },

    /**
     * Function format decimal
     * Author: HMDUC (04/06/2023)
     * @return formatted
     */
    formatInputDecimal(value) {
      let number = this.checkNumber(value);
      if (/^\d+$/.test(number)) {
        var formatted = format.formaDecimal(number);
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
      this.asset.Quantity = parseInt(value) + number;
    },

    /**
     * function handle decrease field number
     * Author: HMDUC (03/06/2023)
     * @param {*} value
     * @return value - 1
     */
    handleDecrease(value, number) {
      if (value > 1) {
        this.asset.Quantity = parseInt(value) - number;
      }
    },

    /**
     * function close form
     * Author: HMDUC (28/05/2023)
     */
    closeForm() {
      this.setIsShow(false);
    },

    /**
     * functin clone Asset
     * Author: HMDUC (28/05/2023)
     */
    cloneAsset() {
      var cloneAsset = {
        AssetCode: this.asset.AssetCode,
        AssetName: this.asset.AssetName,
        DepartmentId: this.asset.DepartmentId,
        DepreciationRate:
          (parseFloat(this.asset.DepreciationRate.replace(",", ".")) * 100) /
          10000,
        DepreciationYear:
          this.asset.DepreciationYear === 0
            ? 0
            : this.asset.DepreciationYear.replace(".", ""),
        Cost:
          this.asset.Cost === 0
            ? 0
            : Number(this.asset.Cost.replace(/\./g, "")),
        CategoryId: this.asset.CategoryId,
        PurchaseDate: this.asset.PurchaseDate,
        Quantity: this.asset.Quantity,
        LifeTime: this.asset.LifeTime,
        ProductionYear: this.asset.ProductionYear,
        TrackedYear: this.yearSelected,
      };
      return cloneAsset;
    },

    /**
     * Function handle btn Cancel form
     * Author: HMDUC (28/05/2023)
     */
    handleCancel() {
      if (this.formMode === Enum.FORM__MODE.EDIT) {
        var assetChanged = {
          ...this.asset,
          DepreciationRate:
            (parseFloat(this.asset.DepreciationRate.replace(",", ".")) * 100) /
            10000,
        };

        if (this.assetJson !== JSON.stringify(assetChanged)) {
          this.$emit("openPopup", "warning", {
            ...this.cloneAsset(),
            AssetId: this.asset.AssetId,
          });
        } else {
          this.closeForm();
        }
      } else if (this.formMode === Enum.FORM__MODE.ADD) {
        this.$emit("openPopup", "warning");
      } else {
        this.$emit("openPopup", "warning", {
          ...this.cloneAsset(),
          DepartmentCode: this.asset.DepartmentCode,
          CategoryCode: this.asset.CategoryCode,
          DepartmentName: this.asset.DepartmentName,
          CategoryName: this.asset.CategoryName,
          DepreciationYear: this.asset.DepreciationYear,
          Cost: this.asset.Cost,
        });
      }
    },

    /**
     * Function check Null Input
     * Author: HMDUC (29/05/2023)
     */
    handleValidate() {
      let isValid = true;
      let firstErrorRef = null;

      for (var inputRef in this.$refs) {
        const self = this.$refs[inputRef];
        const nameLabel = this.$_MISAResources.label__input[self.name];
        if (self.required && !Validate.isEmptyOrNull(self.modelValue)) {
          self.errMesage =
            nameLabel + " " + this.$_MISAResources.text__error.inputErr;
          self.isError = true;
          //get First Input Error
          if (!firstErrorRef) {
            firstErrorRef = self;
          }
          isValid = false;
        } else {
          self.isError = false;
        }
      }
      //Focus first Input Error
      if (firstErrorRef) {
        this.$nextTick(() => {
          firstErrorRef.handleFocus();
        });
      }

      return isValid;
    },

    /**
     * function submit form
     * Author: HMDUC (28/05/2023)
     */
    submitData() {
      //Get formMode current
      let mode = this.formMode;
      if (this.handleValidate()) {
        this.$emit("loading", true);
        switch (mode) {
          //Add Asset
          case Enum.FORM__MODE.ADD:
            this.addAsset(this.cloneAsset());
            break;

          //Edit Asset
          case Enum.FORM__MODE.EDIT:
            var assetChanged = {
              ...this.asset,
              DepreciationRate:
                (parseFloat(this.asset.DepreciationRate.replace(",", ".")) *
                  100) /
                10000,
            };

            //check Asset isChanged
            if (this.assetJson !== JSON.stringify(assetChanged)) {
              this.editAsset({
                ...this.cloneAsset(),
                AssetId: this.asset.AssetId,
              });
            } else {
              setTimeout(() => {
                this.$emit("showToast", "no__edit");
              }, 300);
              this.$emit("loading", false);
            }
            break;

          //Duplicate Asset
          case Enum.FORM__MODE.DUPLICATE:
            this.addAsset(this.cloneAsset());
            break;
        }
      }
    },

    /**
     * Function call Api Add Asset
     * Author: HMDUC (28/05/2023)
     * @param {*} asset
     */
    async addAsset(asset) {
      try {
        const res = await this.postAsset(asset);
        this.handleResponse({
          code: res.status,
          dataErr: res.data.DataError,
        });
      } catch (err) {
        this.handleResponse({ code: err.status });
      }
    },

    /**
     * Function call Api edit Asset
     * Author: HMDUC (28/05/2023)
     * @param {*} asset
     */
    async editAsset(asset) {
      try {
        const res = await this.updateAsset(asset);
        this.handleResponse({
          code: res.status,
          dataErr: res.data.DataError,
        });
      } catch (err) {
        this.handleResponse({ code: err.status });
      }
    },

    /**
     * Funtion handle request respone
     * Author: HMDUC (03/07/2023)
     * @param {*} code
     * @param {*} data
     */
    handleResponse({ code, dataErr }) {
      this.$emit("loading", false);
      switch (code) {
        case Enum.REQ__CODE.CREATED:
          this.$emit("showToast", "success__add");
          this.closeForm();
          break;

        case Enum.REQ__CODE.BAD_REQUEST:
          var arrMessage = [];
          var inputError = [];

          for (var key in dataErr) {
            if (Object.hasOwnProperty.call(dataErr, key)) {
              const message = dataErr[key];
              arrMessage.push(message);

              if (key == "ProductionAndPurchaseDate") {
                key = "PurchaseDate";
              }

              inputError.push(this.$refs[key]);
              this.setListError(inputError);
            }
          }

          this.$emit("openPopup", "error", arrMessage);
          break;

        case Enum.REQ__CODE.SUCCESS:
          this.$emit("showToast", "success__update");
          this.closeForm();
          break;

        case Enum.REQ__CODE.ERROR:
          if (this.formMode === Enum.FORM__MODE.ADD) {
            this.$emit("showToast", "err__add");
          } else {
            this.$emit("showToast", "err__update");
          }
          break;
      }
    },
  },
};
</script>
<style>
@import "@/css/base/form.css";
</style>
