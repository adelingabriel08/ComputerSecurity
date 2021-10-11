#include<stdio.h>
#include<crypt.h>
#include<string.h>
#include<stdlib.h>



int IsPasswordValid(char* password)
{
    char salt[] = "$6$SvT3dVpN$";
    char hash[] = "$6$SvT3dVpN$lwb3GViLl0J0ntNk5BAWe2WtkbjSBMXtSkDCtZUkVhVPiz5X37WflWL4k3ZUusdoyh7IOUlSXE1jUHxIrg29p.";

    printf("%s\n", password);
    return strcmp(hash, crypt(password, salt)) == 0 ? 1 : 0;
}

int IsNonAlphanumeric(char character)
{
    if (character == '!' || character == '@' || character == '#') return 1;

    return 0;
}

int GeneratePassword(int k, char* password, int maxLength)
{
    char characters[] = {'a', 'b', 'c', '1', '2', '!', '@', '#'};
    for(int i = 0; i < 8; i++)
    {
        // if (k > 0 && IsNonAlphanumeric(password[k-1]) && !IsNonAlphanumeric(characters[i]))
        //     return 0;

        password[k] = characters[i];
        if(IsPasswordValid(password))
        {
            printf("Password found: %s\n", password);
            return 1;
        }
        else
            if (k < maxLength-1)
            {
                int found = GeneratePassword(k+1, password, maxLength);

                if (found) return 1;
            }
    }
    return 0;
}

int main()
{
    int length = 1;

    char *password = malloc(length+1);

    while (!GeneratePassword(0, password, length))
    {
        length++;
        password = realloc(password, length+1);
    }

}
