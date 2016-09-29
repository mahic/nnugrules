openssl genrsa -out nnugrules.key 4098
openssl req -new -x509 -sha384 -key nnugrules.key -out nnugrules.cert -days 3650 -subj /CN=nnugrules.no
openssl pkcs12 -export -in nnugrules.cert -inkey nnugrules.key -out nnugrules.pfx