require 'rubygems'
require 'curb'

# This uses the 'curb' libcurl wrapper for ruby, found at https://github.com/taf2/curb/  
# prints data to stdout.

user = "ENTER_USERNAME_HERE"
pass = "ENTER_PASSWORD_HERE"
url = "ENTER_RULES_API_URL_HERE"

rule_value = "ENTER_RULE_VALUE_HERE"

rule = "{\"rules\":[{\"value\":\"" + rule_value + "\"}]}"

Curl::Easy.http_delete(url) do |c|
  c.http_auth_types = :basici
  c.username = user
  c.password = pass
  c.post_body = rule 
  c.verbose = true 

  c.on_body do |data|
    puts data
    data.size # required by curl's api.
  end
end
