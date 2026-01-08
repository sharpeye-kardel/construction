/**
 * @file CreateItem.vue
 * @description View component for creating new construction project items.
 *              Provides a form interface for users to input project details
 *              and submit them to the backend API.
 * @route /create - Route for creating new project items
 *
 * @uses FormComponent - Reusable form component that handles the input fields
 *                       and form submission logic for project data entry
 */
<template>
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Create New Project</v-card-title>
            <v-card-text>
              <FormComponent :formData="formData" :onSubmit="createItem" />
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  import FormComponent from '@/components/FormComponent.vue';
  import { createApiData } from '@/services/dotnetapi'; 
  
  export default {
    components: {
      FormComponent
    },
    /**
     * @data
     * @property {Object} formData - The form data object containing all project fields
     * @property {string} formData.name - Project name, initially empty
     * @property {string} formData.location - Project location, initially empty
     * @property {string} formData.category - Project category, initially empty
     * @property {string} formData.startDate - Project start date, initially empty
     * @property {string} formData.stage - Current project stage, initially empty
     * @property {string} formData.details - Additional project details, initially empty
     * @returns {Object} The initial data state for the component
     */
    data() {
      return {
        formData: {
          name: '',
          location: '',
          category: '',
          startDate: '',
          stage: '',
          details: ''
        }
      };
    },
    methods: {
      /**
       * Creates a new project item by submitting form data to the API.
       * On successful creation, navigates the user back to the home page.
       *
       * @param {Object} data - The project data to be created
       * @param {string} data.name - The name of the project
       * @param {string} data.location - The location of the project
       * @param {string} data.category - The category of the project
       * @param {string} data.startDate - The start date of the project
       * @param {string} data.stage - The current stage of the project
       * @param {string} data.details - Additional details about the project
       * @returns {Promise<void>} Resolves when the item is created and navigation completes
       * @throws {Error} Throws an error if the API call fails
       */
      async createItem(data) {
        await createApiData(data)
          .then(response => {
            this.$router.push('/');
          })
          .catch(error => {
            throw error;
          });
      }
    }
  };
  </script>
  