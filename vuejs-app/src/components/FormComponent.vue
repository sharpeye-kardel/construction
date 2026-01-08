/**
 * @file FormComponent.vue
 * @description A reusable form component for creating and editing construction projects.
 * Provides input fields for project name, location, category, start date, stage, and details
 * with validation rules and error handling.
 *
 * @example
 * <FormComponent :formData="projectData" :onSubmit="handleSubmit" />
 */
<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
    <v-text-field 
      v-model="formData.name" 
      :rules="[
        rules.required, 
        () => !!formData.name && formData.name.length <= 200 || 'Name must be less than 200 characters'
      ]" 
      label="Name"
      counter="200"
      required>
    </v-text-field>
    <v-text-field 
      v-model="formData.location" 
      :rules="[
        rules.required, 
        () => !!formData.location && formData.location.length <= 500 || 'Location must be less than 500 characters'
      ]"
      label="Location" 
      required 
      counter="500">
    </v-text-field>
    <v-select 
      v-model="formData.category" 
      :rules="[rules.required]" 
      :items="categoryOptions" 
      label="Category"
      @change="handleCategoryChange">
    </v-select>
    <v-text-field 
      v-if="formData.category === 'Other'" 
      v-model="formData.otherCategory" 
      label="Please specify"
      placeholder="Enter custom category">
    </v-text-field>
    <v-text-field 
      type="date" 
      v-model="formData.startDate" 
      :rules="[rules.required]" 
      label="Start Date" 
      required
      placeholder="YYYY-MM-DD">
    </v-text-field>
    <v-select 
      v-model="formData.stage" 
      :rules="[rules.required]" 
      :items="stageOptions" 
      label="Stage">
    </v-select>
    <v-textarea 
      v-model="formData.details" 
      :rules="[rules.required]" 
      label="Details" 
      required 
      counter 
      maxlength="2000">
    </v-textarea>

    <v-btn color="success" @click="submitForm" :disabled="!valid">Save</v-btn>
    <v-btn color="secondary" @click="goBack">Back</v-btn>
  </v-form>
  <v-snackbar v-model="snackbar.show" :timeout="snackbar.timeout" color="error">
    {{ snackbar.message }}
  </v-snackbar>
</template>


<script>
export default {
  /**
   * @property {Object} formData - The form data object containing project fields (name, location, category, startDate, stage, details)
   * @required
   * @default {}
   *
   * @property {Function} onSubmit - Callback function invoked when the form is successfully submitted with validated data
   * @required
   */
  props: {
    formData: {
      type: Object,
      default: () => ({}),
      required: true
    },
    onSubmit: {
      type: Function,
      required: true
    }
  },
  /**
   * @data
   * @property {boolean} valid - Tracks whether the form passes all validation rules
   * @property {Object} form - Local copy of form field values for name, startDate, and stage
   * @property {Array} stageOptions - Available construction stage options with value/title pairs (Concept, Design & Documentation, Pre-Construction, Construction)
   * @property {Array} categoryOptions - Available project category options (Education, Health, Office, Other)
   * @property {string} dateFormat - Expected date format pattern for date inputs
   * @property {Object} rules - Validation rule functions including required field validation
   * @property {Object} snackbar - Snackbar notification state with show, message, and timeout properties
   */
  data() {
    return {
      valid: false,
      form: {
        name: this.formData.name || '',
        startDate: this.formData.startDate || '',
        stage: this.formData.stage || null
      },
      stageOptions: [
        { value: 1, title: 'Concept' },
        { value: 2, title: 'Design & Documentation' },
        { value: 3, title: 'Pre-Construction' },
        { value: 4, title: 'Construction' },
      ],
      categoryOptions: ['Education', 'Health', 'Office', 'Other'],
      dateFormat: 'yyyy-MM-dd',
      rules: {
        required: value => !!value || 'Required.',
      },
      snackbar: {
        show: false,
        message: '',
        timeout: 6000
      }
    };
  },
  methods: {
    /**
     * Handles stage selection changes by clearing the otherStage field when a non-Other option is selected.
     * @param {string|number} value - The selected stage value
     * @returns {void}
     */
    handleStageChange(value) {
      if (value !== 'Other') {
        this.formData.otherStage = ''; // Clear 'Other' field if not 'Other'
      }
    },
    /**
     * Navigates back to the previous page in the browser history.
     * @returns {void}
     */
    goBack() {
      this.$router.back(); // Navigate back to the previous page
    },
    /**
     * Validates and submits the form data.
     * Transforms the form data by converting startDate to ISO format and resolving the category value.
     * Displays error messages in a snackbar if submission fails.
     * @returns {Promise<void>}
     */
    async submitForm() {
      if (this.$refs.form.validate()) {
        try {
          const payload = {
            ...this.formData,
            startDate: new Date(this.formData.startDate).toISOString(),
            category: this.formData.category === 'Other' ? this.formData.otherCategory : this.formData.category
          };
          await this.onSubmit(payload);
        } catch (error) {
          let errorMessage = 'An error occurred';
          if (error.response && error.response.data && error.response.data.message) {
            errorMessage = error.response.data.message;
          }
          if (error.response && error.response.data && error.response.data.errors) {
            errorMessage = error.response.data.errors;
          }
          this.snackbar.message = errorMessage;
          this.snackbar.show = true;
        }
      }
    }
  }
};
</script>

<style>
.v-text-field__slot {
  width: 100%;
}
</style>
