<template>
    <section class="section">      
       <div class="columns">
       <div class="column is-4 is-offset-4">
          <h1 class="title is-4 is-spaced">Login</h1>
          <div class='notification is-warning' v-if="errorMessage !=='' ">
              {{ errorMessage }}
          </div>
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
		    <button class="button is-success" @click="onSubmit" :disabled="!isValid">
		      Login
		    </button>
            <button class="button is-warning" @click="logout">
		      Logout
		    </button>
		  </p>
		</div>
        <h5>{{ testname }}</h5>
        <p>
          {{ profile }} 
        </p>
        <p>
            {{ authenticationErrorData }}
        </p>
      </div>         
       </div>
      </section>
</template>
<script>
import { mapGetters, mapActions } from 'vuex'


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
        isValid() { return this.username !== "" && this.password !== "" },
        ...mapGetters('identity',[
            'authenticating',
            'profile',
            'authenticationErrorData',
            'authenticationErrorCode',
            'testname'
        ]),
        errorMessage() {
            if (this.authenticationErrorData != null) {
                let data = this.authenticationErrorData 
                if (data.isLockedOut)
                    return "Account is Locked"
                if (data.isNotAllowed)
                    return "Account is not allowed" 
                
                return "Invalid username or password"
            }
            return ""
        }

    },
    mounted(){
        this.refresh()
    },
    methods: {
        ...mapActions("identity",[
            "login",
        ]),
        refresh(){
            // identity.whoami().then( x => this.currentUser = x.data )
        },
        clear(){
            this.message =''
            this.currentuser = ''
        },
        onSubmit(){
            this.login( { username:this.username, password: this.password})
        },
        // async login() {
        //     this.clear()
        //     try {
        //         await identity.login(this.username, this.password)
        //         this.refresh()
        //     }
        //     catch(error) {
        //         console.log("error on login.vue")
        //     }
        // },
        logout(){
            this.clear()
            identity.logout().then( () => window.location.reload() )
        }
    }

    
}
</script>

