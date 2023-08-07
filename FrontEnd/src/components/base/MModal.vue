<template>
  <div
    class="modal modal--fullscreen"
    v-esc="!isShowFormAsset ? handleCloseModal : ''"
  >
    <div class="modal__content">
      <div class="modal__header">
        <div class="modal--title">
          <h5>{{ titleForm }}</h5>
        </div>
        <m-button
          iconButton="icon__close-v2"
          className="btn__close-v2"
          :title="this.$_MISAResources.tooltip__btn.close"
          @click="handleCloseModal"
        ></m-button>
      </div>
      <div class="modal--body">
        <div class="body__content">
          <div class="title">
            {{ this.$_MISAResources.form__title.ComonInfor }}
          </div>
          <div class="card">
            <div class="card__body">
              <div class="row">
                <div class="col-2">
                  <m-input
                    ref="ReceiptCode"
                    v-model="receipt.ReceiptCode"
                    type="text"
                    name="ReceiptCode"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                    :isFocus="true"
                    :maxLength="20"
                  ></m-input>
                </div>
                <div class="col-2">
                  <m-input
                    v-model="receipt.ReceiptDate"
                    type="date"
                    name="ReceiptDate"
                    className="input__text input__full"
                    ref="ReceiptDate"
                    :required="true"
                    :isLabel="true"
                    @click="log"
                  ></m-input>
                </div>
                <div class="col-2">
                  <m-input
                    v-model="receipt.TranferDate"
                    ref="TranferDate"
                    type="date"
                    name="TranferDate"
                    className="input__text input__full"
                    :required="true"
                    :isLabel="true"
                  ></m-input>
                </div>
                <div class="col-6">
                  <m-input
                    v-model="receipt.Note"
                    type="text"
                    name="Note"
                    className="input__text input__full"
                    :required="false"
                    :isLabel="true"
                    :requiredNot="true"
                  ></m-input>
                </div>
              </div>
              <div class="row flex-wrap">
                <div class="chose__department">
                  <div class="chose--after chose__input d-flex">
                    <input
                      type="checkbox"
                      class="form__check table__checkbox"
                      id="ckboxDepartment"
                      name="ckboxSelectDepartment"
                      v-model="isChecked"
                      @input="handleCheckedAfter($event)"
                      ref="ckboxAfter"
                    />
                    <label class="label__input" for="ckboxDepartment">{{
                      this.$_MISAResources.label__input.ckboxSelectDepartment
                    }}</label>
                  </div>
                  <div
                    class="chose--before chose__input d-flex"
                    v-show="isShowDepartmentBefore"
                  >
                    <m-input
                      type="checkbox"
                      id="ckboxBefore"
                      name="ckboxDepartmentBefore"
                      class="form__check"
                      className="table__checkbox"
                      @input="handleCheckedBefore($event)"
                      ref="ckboxBefore"
                    ></m-input>
                    <label class="label__input" for="ckboxBefore">{{
                      this.$_MISAResources.label__input.ckboxDepartmentBefore
                    }}</label>
                  </div>
                </div>
                <div
                  class="department__append"
                  v-if="isShowDepartmentBefore"
                  ref="componentContainer"
                >
                  <div class="label row">
                    <div class="stt--increament">
                      <m-tooltip
                        :content="this.$_MISAResources.free__text.stt"
                        placement="top"
                      >
                        <template #child>
                          <label for="stt">{{
                            this.$_MISAResources.label__input.Stt
                          }}</label>
                        </template>
                      </m-tooltip>
                    </div>
                    <div class="col-3">
                      <label for="fullName">{{
                        this.$_MISAResources.label__input.FullName
                      }}</label>
                    </div>
                    <div class="col-3">
                      <label for="representation">{{
                        this.$_MISAResources.label__input.Representation
                      }}</label>
                    </div>
                    <div class="col-3">
                      <label for="positionName">{{
                        this.$_MISAResources.label__input.PositionName
                      }}</label>
                    </div>
                  </div>
                  <div
                    class="append"
                    v-for="(componentData, index) in dynamicComponents"
                    :key="index"
                  >
                    <m-depart-dynamic
                      :ref="`dynamic${index}`"
                      :componentData="componentData"
                      :order="index + 1"
                      :isOnlyComponent="isOnlyDynamic"
                      @add="addComponent(dataDynamic)"
                      @show="handleShowAction"
                      @remove="removeComponent"
                      @moveUp="moveComponent"
                    ></m-depart-dynamic>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="detail__action">
            <div class="wrap--left">
              <div class="mg--16 title">
                {{ this.$_MISAResources.form__title.TranferInfor }}
              </div>
            </div>
            <div class="detail__button">
              <m-button
                :content="this.$_MISAResources.content__button.choseAsset"
                className="btn__chose btn__text"
                iconButton="icon__chose"
                @click="openFormChose"
              ></m-button>
            </div>
          </div>
          <div class="body__table">
            <m-table-asset-chose
              :columns="colAssetTranfer"
              :widthDefault="widthColAssetTranfer"
              :dataTable="listAssetTranferChose"
              :numberPage="currentPageTranfer"
              :isFixedAction="true"
              :isShowPagging="true"
              :isShowSummary="true"
              :isShowModal="isShow"
              @changed="changeDepartmentChose"
              @inputReason="handleInputReason"
              @showPopup="handleShowPopup"
            ></m-table-asset-chose>
          </div>
        </div>
      </div>
      <div class="modal--footer">
        <m-button
          type="button"
          :content="this.$_MISAResources.content__button.cancelForm"
          className="btn__sub btn--form-chose btn--form"
          @click="handleCloseModal"
        ></m-button>
        <m-button
          type="button"
          ref="lastBtn"
          :content="this.$_MISAResources.content__button.save"
          className="btn__main btn--primary-modal btn__submit"
          @click="sumitData"
        ></m-button>
      </div>
    </div>
    <!-- Form Chose Asset -->
    <m-dialog v-if="isShowFormAsset">
      <template #content>
        <div class="form__chose" v-esc="closeFormChoseAsset">
          <div class="form__chose--header">
            <div class="modal--title">
              <h5>{{ this.$_MISAResources.form__title.AddReceiptAsset }}</h5>
            </div>
            <m-button
              iconButton="icon__close-v2"
              className="btn__close-v2"
              :title="this.$_MISAResources.tooltip__btn.close"
              @click="closeFormChoseAsset"
            ></m-button>
          </div>
          <div class="form__chose--body">
            <m-table-chose
              :columns="colAssetChose"
              :widthDefault="widthColAssetChose"
              :dataTable="listAssetChose"
              :numberPage="currentPageChose"
              :isShowPagging="true"
              :isShowSummary="true"
              :isTableChose="true"
              :isFixedAction="true"
              @nextPage="nextPage"
              @prevPage="prevPage"
              @pageSize="chosePageSize"
            ></m-table-chose>
            <div class="border--bottom"></div>
            <div class="infor__new">
              <div class="row">
                <div class="col-4">
                  <m-combobox
                    className="select--sm"
                    iconPos="left"
                    :isLabel="
                      this.$_MISAResources.label__input.DepartmentCodeNew
                    "
                    :placeholder="
                      this.$_MISAResources.text__combobox.department
                    "
                    :isFilter="true"
                    :required="true"
                    :listOptions="listDepartment"
                    name="DepartmentCodeNew"
                    v-model="newDepartment"
                    @change="changeDepartment"
                    ref="departmentRef"
                  ></m-combobox>
                </div>
                <div class="col-8">
                  <m-input
                    v-model="newReason"
                    type="text"
                    :placeHolder="this.$_MISAResources.placeholder.Note"
                    name="Note"
                    className="input__text input__full"
                    :required="false"
                    :requiredNot="true"
                    :isLabel="true"
                  ></m-input>
                </div>
              </div>
            </div>
          </div>
          <div class="form__chose--footer">
            <div
              class="selected__wrapper col-6"
              v-show="this.listSelectecAssetClick.length > 0"
            >
              <div>
                {{ this.$_MISAResources.free__text.selected }}
                <strong class="selected__row">{{
                  this.listSelectecAssetClick.length
                }}</strong>
              </div>
              <m-button
                type="button"
                :content="this.$_MISAResources.content__button.clearSelected"
                className=" btn__text clear__selected"
                @click="handleClearSelected"
              ></m-button>
            </div>
            <div class="btn__footer d-flex col-6">
              <m-button
                type="button"
                :content="this.$_MISAResources.content__button.cancelForm"
                className="btn__sub btn--form-chose btn--form"
                @click="closeFormChoseAsset"
              ></m-button>
              <m-button
                type="button"
                ref="lastBtn"
                :content="this.$_MISAResources.content__button.agree"
                className="btn__main btn__submit btn--primary-modal"
                @click="confirmAssetChose"
              ></m-button>
            </div>
          </div>
        </div>
      </template>
    </m-dialog>
    <m-diaglog v-if="isShowPopup">
      <template #content>
        <m-popup
          :type="typePopup"
          :isShow="isShowPopup"
          :errMessage="listMessage"
          :assetError="dataAsset"
          icon="icon__warning"
          @cancel="handleCancel"
          @agree="handleConfirmAgree"
        ></m-popup>
      </template>
      ></m-diaglog
    >
    <m-dialog v-if="isShowLoadingModal">
      <template #content>
        <m-loading></m-loading>
      </template>
    </m-dialog>
    <!-- Toast Message -->
    <m-toast
      :type="typeToast"
      :content="contentToast"
      icon="icon__check"
      :isShow="isShowToast"
    ></m-toast>
  </div>
</template>

<script>
import { request } from "@/services/request";
import Enum from "@/utils/enum";
import { convertRequest, convertToISOString } from "@/utils/format";

import MButton from "@/components/base/MButton";
import MInput from "@/components/base/MInput";
import MTableChose from "@/components/table/MTableChose";
import MPopup from "@/components/base/MPopup";
import MDiaglog from "@/components/base/MDiaglog";
import MTableAssetChose from "@/components/table/MTableAssetChose";
import MDepartment from "@/components/dynamic/MDepartment";
import MTooltip from "@/components/base/MTooltip";
import MToast from "@/components/base/MToast";
import { mapActions, mapState } from "vuex";

export default {
  name: "MModal",
  props: ["isShow", "newReceiptCode", "data", "currentMemberEdit"],
  components: {
    "m-button": MButton,
    "m-input": MInput,
    "m-table-chose": MTableChose,
    "m-table-asset-chose": MTableAssetChose,
    "m-popup": MPopup,
    "m-dialog": MDiaglog,
    "m-depart-dynamic": MDepartment,
    "m-tooltip": MTooltip,
    "m-toast": MToast,
  },
  computed: {
    ...mapState("assetTranferChose", [
      "listAssetTranferChose",
      "isLoadingTableTranfer",
      "listAssetFirst",
    ]),
    ...mapState("assetChose", [
      "listAssetChose",
      "listAssetChoseSelected",
      "listSelectecAssetClick",
    ]),
    ...mapState("modalDialog", ["isShow", "modalMode"]),
    ...mapState("receipt", [
      "listAssetEdit",
      "receiptList",
      "receiptEdit",
      "listAssetDelete",
    ]),

    /**
     * Function set title form by formMode
     * Author: HMDUC (20/5/2023)
     */
    titleForm() {
      if (this.modalMode === Enum.FORM__MODE.ADD) {
        return this.$_MISAResources.form__title["AddReceipt"];
      } else {
        return this.$_MISAResources.form__title["EditReceipt"];
      }
    },
    // eslint-disable-next-line vue/return-in-computed-property
    colAssetTranfer() {
      if (this.modalMode === Enum.FORM__MODE.ADD) {
        return this.$_MISAResources.colTableAssetChoseAdd;
      } else {
        return this.$_MISAResources.colTableAssetChoseEdit;
      }
    },
  },

  created() {
    this.dataList = {
      data: [],
      totalRow: 0,
    };
    this.setListAssetTranferChose(this.dataList);
  },

  mounted() {
    if (this.modalMode === Enum.FORM__MODE.EDIT) {
      let objJson = JSON.stringify(this.receiptEdit);
      let objEditJson = JSON.stringify({
        ReceiptCode: this.receiptEdit.ReceiptCode,
        ReceiptDate: this.receiptEdit.ReceiptDate,
        Note: this.receiptEdit.Note,
        TranferDate: this.receiptEdit.TranferDate,
        ReceiptId: this.receiptEdit.ReceiptId,
        CreatedDate: this.receiptEdit.CreatedDate,
      });
      this.receiptJson = objEditJson;
      this.receipt = JSON.parse(objJson);
      if (this.currentMemberEdit.length > 0) {
        this.isChecked = true;
        this.dynamicComponents.length = 0;
        this.jsonMemberCurrent = JSON.stringify(this.currentMemberEdit);
        this.dynamicComponents = JSON.parse(this.jsonMemberCurrent);
        this.isShowDepartmentBefore = true;
      }
    } else {
      this.receipt = {};
      this.receiptAssets = this.listAssetEdit;
      this.receipt.ReceiptDate = new Date();
      this.receipt.TranferDate = new Date();
      this.receipt.ReceiptCode = this.newReceiptCode;
    }
  },

  updated() {},

  watch: {
    newDepartment(value) {
      console.log(value);
      if (this.modalMode === Enum.FORM__MODE.EDIT) {
        this.arrNewDepartment = this.listAssetTranferChose.data.map((item) => {
          return item.DepartmentReceipt;
        });
        this.arrOldDepartment = this.listAssetTranferChose.data.map((item) => {
          return item.OldDepartmentName;
        });
      } else {
        this.arrNewDepartment = this.dataList.data.map((item) => {
          return item.DepartmentReceipt;
        });
        this.arrOldDepartment = this.dataList.data.map((item) => {
          return item.DepartmentName;
        });
      }
    },
  },

  data() {
    return {
      arrNewDepartment: [],
      arrOldDepartment: [],
      receiptJson: null,
      assetReceiptJson: null,
      widthColAssetTranfer: [45, 50, 130, 170, 188, 188, 200, 188, 188],
      widthColAssetChose: [45, 55, 130, 200, 170, 160, 180, 110],
      isShowPopup: false,
      isShowLoadingModal: false,
      contentToast: null,
      isShowToast: false,
      typePopup: null,
      listMessage: null,
      dataRecieved: null,
      isShowFormAsset: false,
      arrListChoseTranfer: [],
      arrSeleted: [],
      dataList: {},
      arrListId: [],
      listSelectecClick: [],
      listDepartment: null,
      newDepartment: "",
      newReason: "",
      selectedOption: null,
      ids: [],
      currentPageChose: 1,
      pageSizeChose: 20,
      currentPageTranfer: 1,
      colAssetChose: [
        {
          title: "",
          checkbox: true,
          key: this.$_MISAResources.table.key.checkbox,
        },
        {
          title: this.$_MISAResources.table.title.order,
          key: this.$_MISAResources.table.key.order,
          tooltip: this.$_MISAResources.table.title.orderTooltip,
        },
        {
          title: this.$_MISAResources.table.title.assetCode,
          posLeft: true,
          key: this.$_MISAResources.table.key.assetCode,
        },
        {
          title: this.$_MISAResources.table.title.assetName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.assetName,
        },
        {
          title: this.$_MISAResources.table.title.departmentName,
          posLeft: true,
          width: 200,
          key: this.$_MISAResources.table.key.departmentName,
        },
        {
          title: this.$_MISAResources.table.title.cost,
          posLeft: false,
          key: this.$_MISAResources.table.key.cost,
        },
        {
          title: this.$_MISAResources.table.title.residualPrice,
          posLeft: false,
          key: this.$_MISAResources.table.key.residualPrice,
        },
        {
          title: this.$_MISAResources.table.title.trackedYear,
          center: true,
          key: this.$_MISAResources.table.key.trackedYear,
        },
      ],
      receipt: {
        ReceiptId: "",
        ReceiptCode: "",
        ReceiptDate: "",
        TranferDate: "",
        Note: "",
        CreatedDate: "",
      },
      receiptAssets: [],
      isShowDepartmentBefore: false,
      dynamicComponents: [],
      indexDynamic: 0,
      dynamicComponentsData: [],
      dataDynamic: {
        FullName: "",
        Representation: "",
        PositionName: "",
      },
      dataTest: [
        { FullName: "hah", Representation: "dd" },
        { PositionName: "dd" },
      ],
      isOnlyDynamic: false,
      isDuplicateDepartment: false,
      dataMemberPrev: null,
      dataMemberCurrent: null,
      jsonMemberCurrent: null,
      isChecked: false,
      dataAsset: null,
    };
  },

  methods: {
    ...mapActions("assetTranferChose", [
      "setListAssetTranferChose",
      "setLoadingTableTranfer",
    ]),
    ...mapActions("assetChose", [
      "getListAssetChose",
      "setListChoseSelected",
      "setListSelectecAssetClick",
    ]),
    ...mapActions("modalDialog", ["setIsShow", "setModalMode"]),
    ...mapActions("receipt", ["addNewReceipt", "updateOldReceipt"]),
    ...mapActions("inputError", ["setListError"]),

    handleShowPopup(data, type) {
      this.isShowPopup = true;
      this.dataAsset = data;
      this.typePopup = type;
    },

    /**
     * Function get PrevMember
     * Author: HMDUC (05/08/2023)
     */
    async getPrevMember() {
      try {
        const res = await request.get(`Member/GetPrevMember`);
        this.dataMemberPrev = res;
        return res;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function get CurrentMember By ReceiptId
     * Author: HMDUC (05/08/2023)
     */
    async getCurrentMember(id) {
      try {
        const res = await request.getCurrentMember(id);
        this.dataMemberCurrent = res;
        return res;
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * Function add Dynamic Component
     * Author: HMDUC (29/07/2023)
     */
    addComponent(obj) {
      var dataObj = obj
        ? obj
        : {
            FullName: "",
            Representation: "",
            PositionName: "",
          };
      this.indexDynamic++;
      var clonedData = Object.assign({}, dataObj);
      this.dynamicComponents.push(clonedData);
      if (this.dynamicComponents.length === 1) {
        this.$nextTick(() => {
          this.$refs[`dynamic0`][0].isShow = false;
        });
      }
    },

    /**
     * Function remove Dynamic Component
     * Author: HMDUC (29/07/2023)
     */
    removeComponent(index) {
      this.dynamicComponents.splice(index - 1, 1);
      if (this.dynamicComponents.length === 1) {
        this.isOnlyDynamic = true;
      }
      this.indexDynamic--;
      if (this.dynamicComponents.length === 1) {
        this.$nextTick(() => {
          this.$refs[`dynamic0`][0].isShow = false;
        });
      }
    },

    /**
     * Function move Component
     * Author: HMDUC (29/07/2023)
     */
    moveComponent(index, type) {
      if (
        (index === 1 && type === "up") ||
        (index === this.dynamicComponents.length && type === "down")
      ) {
        return;
      }
      var indexRef = index - 1;
      var currentComponent = this.$refs[`dynamic${indexRef}`][0];
      var temp = currentComponent.delivery;
      var dataTemp = this.dynamicComponents[indexRef];

      if (type === "up") {
        var beforeComponent = this.$refs[`dynamic${indexRef - 1}`][0];
        this.$nextTick(() => {
          currentComponent.delivery = beforeComponent.delivery;
          beforeComponent.delivery = temp;
        });
        this.dynamicComponents[indexRef] = this.dynamicComponents[indexRef - 1];
        this.dynamicComponents[indexRef - 1] = dataTemp;
      } else {
        var afterComponent = this.$refs[`dynamic${indexRef + 1}`][0];
        this.$nextTick(() => {
          currentComponent.delivery = afterComponent.delivery;
          afterComponent.delivery = temp;
        });
        this.dynamicComponents[indexRef] = this.dynamicComponents[indexRef + 1];
        this.dynamicComponents[indexRef + 1] = dataTemp;
      }
    },

    /**
     * Function handleChecked input choseDepartment After
     * Author: HMDUC (29/07/2023)
     */
    async handleCheckedAfter(e) {
      this.isShowDepartmentBefore = e.target.checked;
      this.isChecked = e.target.checked;
      if (this.modalMode === Enum.FORM__MODE.ADD) {
        if (this.indexDynamic > 0) {
          this.dynamicComponents.length = 0;
          this.indexDynamic = 0;
        }
        if (e.target.checked) {
          this.addComponent();
        }
        if (!e.target.checked) {
          this.$nextTick(() => {
            this.$refs.ckboxBefore.checked = true;
          });
        }
      } else {
        await this.getCurrentMember(this.receipt.ReceiptId);
        if (this.dataMemberCurrent.length > 0) {
          this.dynamicComponents.length = 0;
          this.jsonMemberCurrent = JSON.stringify(this.dataMemberCurrent);
          this.dynamicComponents = this.dataMemberCurrent;
        } else {
          this.addComponent();
        }
      }
    },

    /**
     * Function handleChecked input choseDepartment Before
     * Author: HMDUC (29/07/2023)
     */
    async handleCheckedBefore(e) {
      await this.getPrevMember();

      this.isOnlyDynamic = false;
      if (e.target.checked) {
        this.dynamicComponents.length = 0;
        this.dataMemberPrev.forEach((item) => {
          this.dynamicComponents.push(item);
        });
      } else {
        this.$nextTick(() => {
          this.$refs[`dynamic0`][0].isShow = false;
        });
        this.dynamicComponents = [
          {
            FullName: "",
            Representation: "",
            PositionName: "",
          },
        ];
      }
      if (this.dynamicComponents.length > 1) {
        this.$nextTick(() => {
          this.$refs[`dynamic0`][0].isShow = true;
        });
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
          value: item.DepartmentCode,
          label: item.DepartmentName,
        }));
        this.listDepartment = mapData;
      } catch (err) {
        this.isShowToast = true;
        this.typeToast = "notice";
        this.contentToast = this.$_MISAResources.toast__content.ErrorServer;
      }
    },

    /**
     * Function check null obj
     * Author: HMDUC (30/07/2023)
     */
    checkNullObj(arrObj) {
      for (const obj of arrObj) {
        for (let key in obj) {
          let value = obj[key];
          if (value === null || value === undefined || value === "") {
            return true;
          }
        }
      }
      return false;
    },

    /**
     * Function change Department Chosed
     * Author: HMDUC (01/08/2023)
     */
    changeDepartmentChose(data, id) {
      this.newDepartment = data;
      this.listAssetTranferChose.data.forEach((item) => {
        if (item.AssetId === id) {
          (item.DepartmentReceipt = data.label),
            (item.DepartmentReceiptId = data.id);
        }
      });
    },

    /**
     *Funcion handle Input Reason by Id
     * @param {*} data
     * @param {*} id
     */
    handleInputReason(data, id) {
      this.listAssetTranferChose.data.forEach((item) => {
        if (item.AssetId === id) {
          item.Reason = data;
        }
      });
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
      this.newDepartment = department;
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
     * Function confirm Asset Tranfer form Chose
     * Author: HMDUC (28/07/2023)
     */
    confirmAssetChose() {
      if (this.listAssetChoseSelected.length > 0) {
        if (!this.newDepartment) {
          this.typePopup = "no__departmentNew";
          this.isShowPopup = true;
          this.$refs.departmentRef.handleFocus();
        } else {
          if (
            this.checkExistId(
              this.newDepartment.id,
              this.listAssetChoseSelected
            )
          ) {
            this.typePopup = "exist__department";
            this.isShowPopup = true;
          } else {
            this.listAssetChoseSelected.forEach((item) => {
              if (this.modalMode === Enum.FORM__MODE.EDIT) {
                (item.Cost = parseInt(item.Cost.replace(/\./g, ""))),
                  (item.ResidualPrice = parseInt(
                    item.ResidualPrice.replace(/\./g, "")
                  ));
              }
              (item.Reason = this.newReason),
                (item.DepartmentReceipt = this.newDepartment.label),
                (item.DepartmentReceiptId = this.newDepartment.id),
                (item.OldDepartmentName = item.DepartmentName),
                (item.OldDepartmentId = item.DepartmentId);
            });
            this.arrSeleted = this.listAssetChoseSelected;
            this.listAssetTranferChose.data.push(...this.arrSeleted);
            this.arrListId = this.listAssetTranferChose.data;
            this.dataList = {
              data: this.arrListId,
              totalRow: this.arrListId.length,
            };
            this.setListAssetTranferChose(this.dataList);
            this.setListSelectecAssetClick([]);
            this.isShowFormAsset = false;
          }
        }
      } else {
        this.typePopup = "no__assetChose";
        this.isShowPopup = true;
      }
    },

    /**
     * Function check duplicate Department
     * Author: HMDUC (29/07/2023)
     */
    checkExistId(id, arr) {
      const isExist = arr.some((item) => item.DepartmentId == id);
      return isExist;
    },

    /**
     * Function confirm close when popup appear
     * Author: HMDUC (28/07/202)
     */
    handleCancel() {
      if (this.typePopup === "warning__modal") {
        this.$emit("closeModal");
      }
      this.isShowPopup = false;
    },

    /**
     * Function confirm agree when popup appear
     */

    handleConfirmAgree() {
      if (!this.isShowFormAsset) {
        this.sumitData();
      }
    },

    /**
     * Function reset formModal
     * Author: HMDUC  (28/07/2002)
     */
    resetModal() {
      this.dynamicComponents.length = 0;
      this.isShowDepartmentBefore = false;
    },
    /**
     * Function close open form detail
     * Author: HMDUC (28/07/2002)
     */
    openFormChose() {
      this.newDepartment = null;
      this.getDepartmentList();
      this.isShowFormAsset = true;
      this.arrListId = this.listAssetTranferChose.data;
      this.ids =
        this.arrListId.length === 0
          ? []
          : this.arrListId.map((item) => item.AssetId);
      this.pageSizeChose = 20;
      this.currentPageChose = 1;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function close form chose asset
     * Author: HMDUC (28/07/2002)
     */
    closeFormChoseAsset() {
      this.isShowFormAsset = false;
      this.setListChoseSelected([]);
      this.setListSelectecAssetClick([]);
    },

    /**
     * Function clear list AssetChose Selected
     * Author: HMDUC (28/07/2002)
     */
    handleClearSelected() {
      this.listSelectecAssetClick.length = 0;
      this.setListSelectecAssetClick([]);
    },

    /**
     * Function next page TableChose
     * Author: HMDUC  (28/07/2023)
     */
    nextPage(page) {
      this.currentPageChose = page;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function prevPage TableChose
     * Author: HMDUC  (28/07/2023)
     */
    prevPage(page) {
      this.currentPageChose = page;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: this.currentPageChose,
      });
    },

    /**
     * Function chosePage Size TableChose
     * Author: HMDUC  (28/07/2023)
     */
    chosePageSize(pageSize) {
      this.pageSizeChose = pageSize;
      this.getListAssetChose({
        ids: this.ids,
        pageSize: this.pageSizeChose,
        pageNumber: 1,
      });
    },

    /**
     * Function clone receipt
     * Author: HMDUC (03/08/2023)
     */
    cloneReceipt() {
      var receiptInsert = {
        ReceiptCode: this.receipt.ReceiptCode,
        ReceiptDate: this.receipt.ReceiptDate,
        Note: this.receipt.Note,
        TranferDate: this.receipt.TranferDate,
      };
      return receiptInsert;
    },

    /**
     * Function check change Form Edit
     * Author: HMDUC (08/07/2023)
     */
    checkChangeEdit() {
      var receiptUpdate = {
        ...this.cloneReceipt(),
        ReceiptId: this.receipt.ReceiptId,
        CreatedDate: this.receipt.CreatedDate,
      };
      var isChangeReceipt = this.receiptJson === JSON.stringify(receiptUpdate);
      var isChangeAsset =
        this.listAssetFirst === JSON.stringify(this.listAssetTranferChose.data);

      if (this.currentMemberEdit?.length > 0) {
        const isChangeMember =
          this.jsonMemberCurrent === JSON.stringify(this.dynamicComponents);
        if (isChangeReceipt && isChangeAsset && isChangeMember) {
          return true;
        }
      }

      if (this.currentMemberEdit?.length === 0) {
        if (this.isChecked) {
          var isChangeMember =
            this.dynamicComponents.length < this.dataMemberCurrent?.length;
          if (isChangeReceipt && isChangeAsset && isChangeMember) {
            return true;
          }
        } else {
          if (isChangeReceipt && isChangeAsset) {
            return true;
          }
        }
      }
      return false;
    },

    /**
     * Function handle Validate Data
     * Author: HMDUC (03/08/2023)
     */
    handleValidate() {
      let isValid = true;
      const isDuplicate = this.arrNewDepartment.some(
        (item, index) =>
          this.arrOldDepartment.includes(item) &&
          this.arrOldDepartment.indexOf(item) === index
      );
      if (this.modalMode === Enum.FORM__MODE.EDIT) {
        const timeTranferDate = convertRequest(this.receipt.TranferDate);
        const timeReceiptDate = convertRequest(this.receipt.ReceiptDate);
        if (timeReceiptDate > timeTranferDate) {
          const arrMsg = [];
          this.isShowPopup = true;
          this.typePopup = "receipt__date";
          this.typePopup = "receipt__date";
          const self = this.$refs.TranferDate;
          arrMsg.push(self);
          this.setListError(arrMsg);
          return false;
        }
      } else {
        if (this.receipt.ReceiptDate > this.receipt.TranferDate) {
          const arrMsg = [];
          this.isShowPopup = true;
          this.typePopup = "receipt__date";
          const self = this.$refs.TranferDate;
          arrMsg.push(self);
          this.setListError(arrMsg);
          return false;
        }
      }
      if (this.receiptAssets.length === 0) {
        this.isShowPopup = true;
        this.typePopup = "no__assetChose";
        return false;
      }
      if (isDuplicate) {
        this.isShowPopup = true;
        this.typePopup = "dupli_depart";
        return false;
      }
      if (this.isChecked) {
        if (
          this.dynamicComponents.length > 0 &&
          this.checkNullObj(this.dynamicComponents)
        ) {
          this.isShowPopup = true;
          this.typePopup = "no__member";
          return false;
        }
      }

      return isValid;
    },
    /**
     * Function close Modal Detail
     * Author: HMDUC (28/07/2023)
     */
    handleCloseModal() {
      if (this.modalMode === Enum.FORM__MODE.ADD) {
        this.typePopup = "warning__modal";
        this.isShowPopup = true;
      } else {
        if (!this.checkChangeEdit()) {
          this.typePopup = "warning__modal";
          this.isShowPopup = true;
        } else {
          this.$emit("closeModal");
        }
      }
      this.setListChoseSelected([]);
    },

    /**
     * Function handle submit data
     *  Author: HMDUC  (28/07/2023)
     */
    sumitData() {
      this.receiptAssets = this.listAssetTranferChose.data;
      this.isShowLoadingModal = true;
      setTimeout(() => {
        this.isShowLoadingModal = false;
        if (this.handleValidate()) {
          let mode = this.modalMode;
          switch (mode) {
            //Add Receipt
            case Enum.FORM__MODE.ADD:
              var dataInsertDto = {};
              this.dynamicComponents.forEach((obj, index) => {
                obj.STT = index + 1;
              });
              dataInsertDto = {
                receiptInsertDto: this.cloneReceipt(),
                receiptAssets: this.receiptAssets,
                members: this.dynamicComponents,
              };
              this.addReceipt(dataInsertDto);
              break;
            case Enum.FORM__MODE.EDIT:
              //check change form edit
              if (this.checkChangeEdit()) {
                this.isShowToast = true;
                this.typeToast = "no__edit_receipt";
                setTimeout(() => {
                  this.isShowToast = false;
                }, 2000);
              } else {
                var dataUpdateDto = {};
                this.dynamicComponents.forEach((obj, index) => {
                  obj.STT = index + 1;
                });
                dataUpdateDto = {
                  receiptUpdateDto: {
                    ...this.cloneReceipt(),
                    ReceiptId: this.receipt.ReceiptId,
                    ReceiptDate: convertToISOString(this.receipt.ReceiptDate),
                    TranferDate: convertToISOString(this.receipt.TranferDate),
                    CreatedDate: this.receipt.CreatedDate,
                  },
                  receiptAssets: this.receiptAssets,
                  members: this.dynamicComponents,
                  listDelete: this.listAssetDelete,
                };
                this.updateReceipt(dataUpdateDto);
              }
          }
        }
      }, 300);
    },
    /**
     * Function call Api add new Receipt
     * Author: HMDUC (31/07/2023)
     */
    async addReceipt(receipt) {
      try {
        const res = await this.addNewReceipt(receipt);
        this.handleResponse({
          code: res.status,
          dataErr: res.data.DataError,
        });
      } catch (err) {
        this.handleResponse({ code: err.status });
      }
    },

    /**
     * Function call Api update new Receipt
     * Author: HMDUC (03/08/2023)
     */
    async updateReceipt(receipt) {
      try {
        const res = await this.updateOldReceipt(receipt);
        this.handleResponse({
          code: res.status,
          dataErr: res.data.DataError,
        });
      } catch (err) {
        this.handleResponse({ code: err.status });
      }
    },

    /**
     * Function handle Response
     * Author: HMDUC (03/08/2023)
     */
    handleResponse({ code, dataErr }) {
      this.resetModal();
      this.$emit("loading", false);
      switch (code) {
        case Enum.REQ__CODE.CREATED:
          this.$emit("showToast", "success__add_receipt");
          this.setIsShow(false);
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
          this.isShowPopup = true;
          this.typePopup = "error";
          this.listMessage = arrMessage;
          break;
        case Enum.REQ__CODE.SUCCESS:
          this.$emit("showToast", "success__update_receipt");
          this.setIsShow(false);
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

<style scoped>
@import "@/css/base/modal.css";
.table__view {
  justify-content: unset;
  padding: unset;
  height: 100%;
}
.label {
  gap: 0 15px;
  margin-bottom: 10px;
}
.label > div:not(:first-child) {
  margin-left: 1px;
}

.label > div:nth-child(2) {
  margin-left: 5px;
}

.label > div:nth-child(4) {
  margin-left: -2px;
}

.label > div > label {
  font-family: RobotoRegular;
  font-size: 14px;
}
</style>
