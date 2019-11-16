<template>
    <v-app>
        <div class="dashboard ml-5 mt-2 ">
            <v-container>
                <v-layout row wrap>
                <h1 class="grey--text heder_apb">List of Dialog</h1>
                <v-col class="d-flex select">
        <v-select
          :items="items"
          v-model="chapter"
          label="Select Chapter"
          outlined
        ></v-select>
      </v-col>
                </v-layout>
            </v-container>
            <v-container class="card_dialog">
                <v-card flat class="pa-3" v-for="script in scripts" :key="script.script_desc">
                    <v-layout row wrap>
                        <v-flex xs6 sm4 md2 class="ml-3">
                            <div class="caption grey--text ml-3">No.</div>
                            <div class="ml-3 mt-3">{{script.script_id}}</div>
                        </v-flex>
                        <v-flex xs10 md5>
                            <div class="caption grey--text ml-3">Description</div>
                            <div class="ml-3 mt-3">{{script.script_desc}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text ml-5">Role</div>
                            <div class="ml-5 mt-3">{{script.script_role}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text ml-3">Event Scipt</div>
                            <div class="ml-3 mt-3">{{script.script_event}}</div>
                        </v-flex>
                    </v-layout>
                </v-card>
            </v-container>
        </div>
        
    </v-app>
    
</template>

<script>
import axios from "axios";

export default {
    data() { 
        return {
            scripts: null,
            chapter: '',
            items: ['Chapter1', 'Chapter2', 'Chapter3', 'Chapter4', 'Chapter5', 'Chapter6', 'Chapter7', 'Chapter8'],
        }     
    },
    mounted() {
            axios
            .get("https://it59-28yomimasu.firebaseio.com/Content/Chapter/"+this.chapter+"/Scripts.json").then(response =>{
                this.scripts = response.data
            })
    }
}
</script>

<style>
.select {
    margin-left: 10px; 
    max-width : 200px;
}

.card_dialog {
    margin-top: -30px; 
}
</style>