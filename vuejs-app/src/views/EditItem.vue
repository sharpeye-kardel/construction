/**
 * @file EditItem.vue
 * @description View component for editing existing construction project items.
 *              Loads project data by ID from the API and provides a form interface
 *              for users to modify and save project details.
 * @route /edit/:id - Route for editing a specific project item by its ID
 *
 * @uses FormComponent - Reusable form component that handles the input fields
 *                       and form submission logic for project data editing
 */
<template>
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Edit Project</v-card-title>
            <v-card-text>
              <FormComponent :formData="formData" :onSubmit="editItem" />
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  import FormComponent from '@/components/FormComponent.vue';
  import { getApiDataById } from '@/services/symfonyapi';
  import { updateApiData } from '@/services/dotnetapi'; 
  
  export default {
    components: {
      FormComponent
    },
    /**
     * @data
     * @property {Object} formData - The form data object containing all project fields
     * @property {number|null} formData.id - Project unique identifier, initially null
     * @property {string} formData.name - Project name, initially empty
     * @property {string} formData.location - Project location, initially empty
     * @property {string} formData.category - Project category, initially empty
     * @property {string} formData.startDate - Project start date in YYYY-MM-DD format, initially empty
     * @property {string} formData.stage - Current project stage, initially empty
     * @property {string} formData.details - Additional project details, initially empty
     * @returns {Object} The initial data state for the component
     */
    data() {
      return {
        formData: {
          id: null,
          name: '',
          location: '',
          category: '',
          startDate: '',
          stage: '',
          details: '',
        }
      };
    },
    /**
     * Lifecycle hook called when the component is created.
     * Fetches the project data by ID from the route parameters and populates the form.
     * Formats the startDate to YYYY-MM-DD format for the date input field.
     *
     * @returns {void}
     */
    created() {
      const id = this.$route.params.id;
      getApiDataById(id)
        .then(response => {
          this.formData = response.data;
          this.formData.startDate = response.data.startDate.split('T')[0];
        })
        .catch(error => {
          console.error('Error fetching item:', error);
        });
    },
    methods: {
      /**
       * Updates an existing project item by submitting modified form data to the API.
       * On successful update, navigates the user back to the home page.
       *
       * @param {Object} data - The project data to be updated
       * @param {number} data.id - The unique identifier of the project
       * @param {string} data.name - The name of the project
       * @param {string} data.location - The location of the project
       * @param {string} data.category - The category of the project
       * @param {string} data.startDate - The start date of the project
       * @param {string} data.stage - The current stage of the project
       * @param {string} data.details - Additional details about the project
       * @returns {Promise<void>} Resolves when the item is updated and navigation completes
       * @throws {Error} Throws an error if the API call fails
       */
      async editItem(data) {
        await updateApiData(data.id, data)
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
  