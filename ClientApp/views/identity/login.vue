<template>
    <section class="section">      
       <div class="columns">
       <div class="column is-4 is-offset-4">
		  <div class="field">
		  <p class="control has-icons-left has-icons-right">
		    <input class="input" type="text" placeholder="username" v-model="username">
		    <span class="icon is-small is-left">
		      <i class="fa fa-user"></i>
		    </span>
		    <span class="icon is-small is-right">
		      <i class="fa fa-check"></i>
		    </span>
		  </p>
		</div>
		<div class="field">
		  <p class="control has-icons-left">
		    <input class="input" type="password" placeholder="Password" v-model="password">
		    <span class="icon is-small is-left">
		      <i class="fa fa-lock"></i>
		    </span>
		  </p>
		</div>
		<div class="field">
		  <p class="control">
		    <button class="button is-success" @click="login" :disabled="!isValid">
		      Login
		    </button>
		  </p>
		</div>
        <p>
          {{ currentUser }}
        </p>
        <p>
            {{ message }}
        </p>
      </div>         
       </div>
      </section>
</template>
<script>
import identity from '../../api/identity'

export default {
    name: 'identity-login',
    data() {
        return {
            username: '',
            password: '',
            currentUser: {},
            message: null
        }
    },
    computed:{
        isValid() { return this.username !== "" && this.password !== "" }
    },
    mounted(){
        identity.whoami().then( x => this.currentUser = x.data )
    },
    methods: {
        login() {
            this.message = '' 
            this.currentUser = ''
            identity.login(this.username, this.password)
                .then( x => this.currentUser = x.data )
                .catch( x => this.message = x.response.data )
        }
    }

    
}
</script>

